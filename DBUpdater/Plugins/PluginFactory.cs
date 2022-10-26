using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbUpdater.Plugins
{
    public static class PluginFactory
    {
        public static IDbUpdaterPlugin Parse(string str)
        {
            var result = new List<IDbUpdaterPlugin>();

            if (!string.IsNullOrWhiteSpace(str))
            {
                var names = str.Split('|');
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(IDbUpdaterPlugin).IsAssignableFrom(x));

                Console.WriteLine("The following plugin(s) found:");

                foreach (var name in names)
                {
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        continue;
                    }

                    var parts = name.Split(':');
                    var typeName = parts[0];
                    string[] parameters = new string[0];

                    if (parts.Length > 1)
                    {
                        var parametersString = parts[1];
                        parameters = parts[1].Split(',');
                    }

                    var type = types.First(t => t.Name == typeName);
                    result.Add(Activator.CreateInstance(type, parameters) as IDbUpdaterPlugin);

                    Console.WriteLine($"  {typeName}");
                }
            }

            return new PluginContainer(result);
        }
    }
}
