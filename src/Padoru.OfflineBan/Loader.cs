using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.OfflineBan
{
    public sealed class Loader : ILoader
    {
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.High)]
        [PluginEntryPoint("Padoru.OfflineBan", "2022.1223.0", "Офбан", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this, Config);
        }
    }
}