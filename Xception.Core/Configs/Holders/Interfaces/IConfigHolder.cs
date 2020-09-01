using Xception.Core.Configs;

namespace Xception.Xception.Core.Configs.Holders.Interfaces
{
    public interface IConfigHolder
    {
        void Load(Config config);
        void Save(Config config);
    }
}
