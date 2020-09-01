using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xception.Core.Interfaces;
using Xception.Xception.Core.Configs;

namespace Xception.Core.Configs
{
    public abstract class Config : IConfig
    {
        public Config()
        {
            GetType().GetProperties().ToList().ForEach(GetProperty);
        }

        private readonly ConcurrentDictionary<string, object> Configuration = new ConcurrentDictionary<string, object>();

        internal Dictionary<string, PropertyInfo> Properties = new Dictionary<string, PropertyInfo>();

        internal IReadOnlyCollection<PropertyInfo> AllProperties => Properties.Values.ToList().AsReadOnly();

        public object this[string key] { get => Configuration[key]; set { Configuration[key] = value; } }

        public bool Contains(string key) => Configuration.ContainsKey(key);

        public void Load()
        {
            OnLoadEvent();
        }

        public void Save()
        {
            OnSaveEvent();
        }

        public event EventHandler<EventArgs> OnLoad;
        public event EventHandler<EventArgs> OnSave;
        public event EventHandler<ConfigUpdatedEventArgs> OnUpdate;

        private void OnLoadEvent() => OnLoad(this, EventArgs.Empty);

        private void OnSaveEvent() => OnLoad(this, EventArgs.Empty);

        private void OnUpdateEvent(string name) => OnUpdate(this, new ConfigUpdatedEventArgs(name));

        private void GetProperty(PropertyInfo propertyInfo)
        {
            var attr = (ConfigNode)Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigNode));
            if (attr != null)
            {
                Properties.Add(attr.GetKey(), propertyInfo);

                var defAttr = (DefaultValueAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DefaultValueAttribute));
                if (defAttr != null)
                {
                    Configuration[attr.GetKey()] = defAttr.Value;
                }
                else
                {
                    Type targetType = propertyInfo.PropertyType;
                    Configuration[attr.GetKey()] = !targetType.IsValueType ? null : Activator.CreateInstance(targetType);
                }
            }
        }
    }
}
