using Padoru.Lib.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Kit
{
    public class Loader : ILoader<Config>
    {
        [field: PluginConfig]
        public Config Config { get; set; }
        
        [PluginPriority(LoadPriority.High)]
        [PluginEntryPoint("Padoru.Kit", "2022.1223.0", "Общие утилиты для наших серверов", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this);
        }
    }
}