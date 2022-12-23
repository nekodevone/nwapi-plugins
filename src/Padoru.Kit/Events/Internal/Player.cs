using System.Linq;
using NorthwoodLib.Pools;
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
            const string orange = "#f3ae84";
            const string red = "#f23c34";

            var adminList =
                ListPool<PlayerAPI>.Shared.Rent(PlayerAPI.GetPlayers().Where(player => player.RemoteAdminAccess));

            var text = $"<color={orange}><size=36>Репорт от <color={red}>[{issuer.PlayerId}] {issuer.Nickname}</color> на <color={red}>[{target.PlayerId}] {target.Nickname}</color></size></color>:<br><size=28>{reason}</size>";

            foreach (var admin in adminList)
            {
                admin.SendBroadcast(text, 5, Broadcast.BroadcastFlags.Truncated);
            }

            ListPool<PlayerAPI>.Shared.Return(adminList);
        }
    }
}