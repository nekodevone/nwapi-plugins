using Padoru.API.Features.Plugins;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Kit.Events.Internal
{
    public class Player : IEventHandler
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerAPI player)
        {
            Log.Info($"Player {player.Nickname} ({player.UserId}) joined the server from {player.IpAddress}");
        }

        [PluginEvent(ServerEventType.PlayerReport)]
        public void OnPlayerReport(PlayerAPI issuer, PlayerAPI target, string reason)
        {
            Log.Info($"Player {issuer.Nickname} ({issuer.UserId}) reported {target.Nickname} ({target.UserId}) for {reason}");
        }
    }
}