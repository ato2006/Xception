using System;

namespace Xception.Xception.Core.Configs
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ConfigNode : Attribute
    {
        public ConfigNode(string key) => Key = key;
        public ConfigNode(string key, string sMethodName, string dMethodName) : this(key)
        {
            SerializeMethodName = sMethodName;
            DeserializeMethodName = dMethodName;
        }

        private readonly string Key;
        private readonly string SerializeMethodName;
        private readonly string DeserializeMethodName;

        public string GetKey() => Key;
        public string GetSerializeMethodName() => SerializeMethodName;
        public string GetDeserializeMethodName() => DeserializeMethodName;
    }
}
