using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xception.Core.Configs;
using Xception.Core.Factories;
using Xception.Core.Interfaces;

namespace Xception.Core.Settings
{
    public static class ConfigManager
    {
        private static IConfig _config;
        private static readonly object _lock = new object();

        public static IConfig Config
        {
            get
            {
                lock (_lock)
                {
                    return _config;
                }
            }
        }

        public static T GetConfig<T>() where T : IConfig
        {
            lock (_lock)
            {
                return (T)_config;
            }
        }

        public static void SetConfig<T>(string file) where T : Config
        {
            var holder = new ConfigHolder(file);
            _config = Factory.CreateConfig<T>(holder);

        }

        public static void LoadConfig()
        {
            lock (_lock)
            {
                if (_config != null)
                    _config.Load();
                else
                    throw new Exception("Empty config file.");
            }
        }

        public static void SaveConfig()
        {
            lock (_lock)
            {
                if (_config != null)
                    _config.Save();
                else
                    throw new Exception("Empty config file.");
            }
        }
    }
}
