using Padoru.API.Features.Plugins;
using Padoru.OfflineBans.Classes;
using PluginAPI.Core;
using System.IO;

namespace Padoru.OfflineBans
{
    public sealed class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();

        public static Config Configs => Instance.Config;

        protected override void OnLoaded()
        {
            Log.Info("Hurray, PadoruKit is loaded!");

            if (!Directory.Exists(Tools.FolderPath))
            {
                Directory.CreateDirectory(Tools.FolderPath);
            }
        }
    }
}