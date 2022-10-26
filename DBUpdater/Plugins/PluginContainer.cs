using System;
using System.Collections.Generic;

namespace DbUpdater.Plugins
{
    public class PluginContainer : IDbUpdaterPlugin
    {
        private readonly List<IDbUpdaterPlugin> _plugins; 

        public PluginContainer(List<IDbUpdaterPlugin> plugins)
        {
            _plugins = plugins;
        }

        public void ExecuteBeforeUpdate(string connectionString, Dictionary<string, string> variables, string scriptSource)
        {
            Console.WriteLine("Executing plugin(s) before the Update");

            foreach (var p in _plugins)
            {
                p.ExecuteBeforeUpdate(connectionString, variables, scriptSource);
            }
        }
        public void ExecuteAfterUpdate(string connectionString, Dictionary<string, string> variables, string scriptSource)
        {
            Console.WriteLine("Executing plugin(s) after the Update");

            foreach(var p in _plugins)
            {
                p.ExecuteAfterUpdate(connectionString, variables, scriptSource);
            }
        }
    }
}
