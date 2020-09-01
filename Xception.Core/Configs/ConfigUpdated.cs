using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xception.Core.Configs
{
    public class ConfigUpdatedEventArgs : EventArgs
    {
        public string Name { get; set; }

        public ConfigUpdatedEventArgs(string name)
        {
            this.Name = name;
        }
    }
}
