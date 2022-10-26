using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DbUpdater.Plugins
{
    public class IsolatedViewsPlugin : IDbUpdaterPlugin
    {
        private readonly string _sourceSchema;
        private readonly string _viewSchema;
        private static string _tenantColumn = "tenant_id";
        private readonly IEnumerable<string> _systemColumns = new string[] { _tenantColumn, "created", "created_by" };

        public IsolatedViewsPlugin(string srcSchema, string vwSchema)
        {
            _sourceSchema = srcSchema;
            _viewSchema = vwSchema;
        }

        public void ExecuteBeforeUpdate(string connectionString, Dictionary<string, string> variables, string scriptSource)
        {
        }

        public void ExecuteAfterUpdate(string connectionString, Dictionary<string, string> variables, string scriptSource)
        {
            Console.WriteLine($"Executing plugin {nameof(IsolatedViewsPlugin)}:");
            var result = new StringBuilder();
            var tables = GetTables(connectionString);

            foreach(var table in tables)
            {
                Console.WriteLine($"  creating {_viewSchema}.{table}...");
                var columns = GetColumns(connectionString, table);
                ExecuteSql(connectionString, GetDropViewSql(table));
                ExecuteSql(connectionString, GetCreateViewSql(table, columns));
                ExecuteSql(connectionString, $"ALTER AUTHORIZATION ON {_viewSchema}.{table} TO dbo;");
                Console.WriteLine($"  View created: {_viewSchema}.{table}");
            }

            Console.WriteLine($"Plugin {nameof(IsolatedViewsPlugin)} finished successfully.");
        }

        private void ExecuteSql(string connectionString, string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetDropViewSql(string table)
        {
            var result = new StringBuilder();
            string viewName = table;
            string viewFullName = $"{_viewSchema}.{table}";
            result.AppendLine($"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = '{viewName}' AND TABLE_SCHEMA = '{_viewSchema}') DROP VIEW {viewFullName};");
            return result.ToString();
        }

        private string GetCreateViewSql(string table, string[] columns)
        {
            var result = new StringBuilder();
            string viewName = table;
            string viewFullName = $"{_viewSchema}.{table}";

            result.AppendLine($"CREATE VIEW {viewFullName}");
            result.AppendLine($"AS");

            string columnList = "";
            bool isMultiTenant = false;

            foreach(var column in columns)
            {
                if(_systemColumns.Contains(column))
                {
                    if (column == _tenantColumn)
                    {
                        isMultiTenant = true;
                    }

                    continue;
                }

                var comma = columnList == "" ? "" : ", ";
                columnList = $"{columnList}{comma}{column}"; 
            }

            result.AppendLine($"    SELECT {columnList}");
            result.AppendLine($"    FROM {_sourceSchema}.{table}");

            if(isMultiTenant)
            {
                result.AppendLine($"    WHERE {_tenantColumn} = [dbo].[GetTenantId]()");
            }

            result.AppendLine($";");

            return result.ToString();
        }

        private string[] GetColumns(string connectionString, string table)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}' AND TABLE_SCHEMA = '{_sourceSchema}'", connection))
                {
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    var result = new List<string>();

                    while (reader.Read())
                    {
                        result.Add(reader["COLUMN_NAME"].ToString());
                    }

                    return result.ToArray();
                }
            }
        }

        private string[] GetTables(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_SCHEMA = '{_sourceSchema}' AND LEFT(TABLE_NAME, 1) <> '_'", connection))
                {
                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    var result = new List<string>();

                    while (reader.Read())
                    {
                        result.Add(reader["TABLE_NAME"].ToString());
                    }

                    return result.ToArray();
                }
            }
        }
    }
}
