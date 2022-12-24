using Padoru.API.Features.Plugins;
using Padoru.Kit.API.Features.Jails;
using Padoru.Kit.API.Features.Reports;
using PluginAPI.Core;

namespace Padoru.Kit
{
    public sealed class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();

        public static Config Configs => Instance.Config;

        public static ReportsController Reports { get; } = new();

        public static JailController Jail { get; } = new();

        protected override void OnLoaded()
        {
            Log.Info("Hurray, PadoruKit is loaded!");
        }
    }
}