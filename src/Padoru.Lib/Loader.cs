using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Lib
{
    public class Loader : LoaderBase<Config>
    {
        [PluginConfig]
        public new Config Config;

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("Padoru.Lib", "2022.1223.0", "Библиотека для плагинов", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this);
        }
    }
}