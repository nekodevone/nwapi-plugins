using Padoru.Lib.API.Features.Plugins;
using PluginAPI.Core;

namespace Padoru.Kit
{
    public class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();

        public override void OnLoaded()
        {
            Log.Info("Hurray, PadoruKit is loaded!");
        }
    }
}