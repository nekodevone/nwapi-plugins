using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Logger
{
    public class Loader : ILoader
    {
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("Padoru.Logger", "2022.1224.0", "Логирование событий", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this, Config);
        }
    }
}