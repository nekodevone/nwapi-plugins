using Padoru.API.Features.Plugins;
using PluginAPI.Core;

namespace Padoru.Logger
{
    public class Plugin : PluginBase<Config>
    {
        public static Plugin Instance { get; } = new();
        
        public static Sender Sender => Instance._sender;
        
        private Sender _sender;

        protected override void OnLoaded()
        {
            if (string.IsNullOrEmpty(Config.WebhookUrl))
            {
                Log.Error("URL вебхука не установлен в конфиге!");
                return;
            }

            _sender = new Sender();
        }
    }
}