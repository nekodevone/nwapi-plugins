using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Kit
{
    public sealed class Loader : ILoader
    {
        [PluginConfig]
        public Config Config;
        
        [PluginPriority(LoadPriority.High)]
        [PluginEntryPoint("Padoru.Kit", "2023.0119.0", "Общие утилиты для наших серверов", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this, Config);
        }
    }
}