using Padoru.API.Features.Plugins;
using PluginAPI.Core;

namespace Padoru.Lib
{
    public sealed class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();

        public static Config Configs => Instance.Config;

        protected override void OnLoaded()
        {
            Log.Info("Hurray, PadoruLib is loaded!");
        }
    }
}