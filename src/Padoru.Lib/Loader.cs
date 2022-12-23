using Padoru.Lib.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Lib
{
    public class Loader : ILoader<Config>
    {
        [field: PluginConfig]
        public Config Config { get; set; }

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("Padoru.Lib", "2022.1223.0", "Библиотека для плагинов", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this);
        }
    }
}