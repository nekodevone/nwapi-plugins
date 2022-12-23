using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Donate
{
    public sealed class Loader : ILoader
    {
        [PluginConfig]
        public Config Config;
        
        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("Padoru.Donate", "2022.1223.0", "Платные фичи для поддержавших проект", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this, Config);
        }
    }
}