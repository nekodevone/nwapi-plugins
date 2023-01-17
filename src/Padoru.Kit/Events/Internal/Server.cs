using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Kit.Events.Internal
{
    public class Server : IEventHandler
    {
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void ClearReportsOnWaitingForPlayers()
        {
            Plugin.Reports.Clear();
        }
        
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void ClearJailOnWaitingForPlayers()
        {
            Plugin.Jail.Clear();
        }
    }
}