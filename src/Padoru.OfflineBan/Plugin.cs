using Padoru.API.Features.Plugins;
using System.IO;
using Padoru.OfflineBan.Utils;

namespace Padoru.OfflineBan
{
    public sealed class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();

        public static Config Configs => Instance.Config;

        protected override void OnLoaded()
        {
            if (!Directory.Exists(Tools.FolderPath))
            {
                Directory.CreateDirectory(Tools.FolderPath);
            }
        }
    }
}