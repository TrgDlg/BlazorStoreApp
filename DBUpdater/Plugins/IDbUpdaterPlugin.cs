using System.Collections.Generic;

namespace DbUpdater.Plugins
{
    public interface IDbUpdaterPlugin
    {
        void ExecuteBeforeUpdate(string connectionString, Dictionary<string, string> variables, string scriptSource);
        void ExecuteAfterUpdate(string connectionString, Dictionary<string, string> variables, string scriptSource);
    }
}
