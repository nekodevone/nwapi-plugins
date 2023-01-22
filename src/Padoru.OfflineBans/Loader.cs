using Padoru.API.Features.Plugins;
using Padoru.OfflineBans.Classes;
using Padoru.OfflineBans.Events;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using System.IO;

namespace Padoru.OfflineBans
{
    public sealed class Loader : ILoader
    {
        [PluginConfig]
        public Config Config;
        
        [PluginPriority(LoadPriority.High)]
        [PluginEntryPoint("Padoru.OfflineBans", "2022.1223.0", "Офбан", "NekoDev Team")]
        public void Load()
        {
            Plugin.Instance.Load(this, Config);

            if (!Directory.Exists(Tools.FolderPath))
            {
                Directory.CreateDirectory(Tools.FolderPath);
            }
        }
    }
}