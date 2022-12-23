using Padoru.API.Features.Plugins;

namespace Padoru.Donate
{
    public sealed class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();

        public static Config Configs => Instance.Config;

        protected override void OnLoaded()
        {
        }
    }
}