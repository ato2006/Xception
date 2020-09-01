using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xception.Core.Interfaces
{
    public interface IConfig
    {
        object this[string key] { get; set; }

        void Load();
        void Save();
    }
}
