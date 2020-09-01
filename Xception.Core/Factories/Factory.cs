using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xception.Core.Configs;
using Xception.Xception.Core.Configs.Holders.Interfaces;

namespace Xception.Core.Factories
{
    public static class Factory
    {
        public static T CreateConfig<T>(IConfigHolder holder) where T : Config
        {
            var ctor = typeof(T).GetConstructor((BindingFlags)Bindings.All, Type.DefaultBinder, new Type[] { }, null);

            T config = (T)ctor.Invoke(new object[0]);
            // config.Holder = holder;

            return config;
        }
    }
}
