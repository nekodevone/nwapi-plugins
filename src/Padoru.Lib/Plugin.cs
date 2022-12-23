using Padoru.API.Features.Plugins;
using PluginAPI.Core;

namespace Padoru.Lib
{
    public class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();

        public static Config Configs => Instance.Config;

        public override void OnLoaded()
        {
            Log.Info("Hurray, PadoruLib is loaded!");
        }
    }
}