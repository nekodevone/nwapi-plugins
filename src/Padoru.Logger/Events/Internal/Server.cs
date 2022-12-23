using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Logger.Events.Internal
{
    public class Server : IEventHandler
    {
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void OnWaitingForPlayers()
        {
            Plugin.Sender.AddToQuene("Сервер ожидает игроков");
        }

        [PluginEvent(ServerEventType.RoundRestart)]
        public void OnRoundRestart()
        {
            Plugin.Sender.AddToQuene("Раунд рестартится");
        }

        [PluginEvent(ServerEventType.RoundStart)]
        public void OnRoundStart()
        {
            Plugin.Sender.AddToQuene("Раунд начался");
        }
    }
}