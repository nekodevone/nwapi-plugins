using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Kit
{
    public class Loader : LoaderBase<Config>
    {
        [PluginConfig]
        public new Config Config;
        
        [PluginPriority(LoadPriority.High)]
        [PluginEntryPoint("Padoru.Kit", "2022.1223.0", "Общие утилиты для наших серверов", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this);
        }
    }
}