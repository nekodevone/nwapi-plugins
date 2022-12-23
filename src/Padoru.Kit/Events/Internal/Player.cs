using MEC;
using Padoru.API.Features.Plugins;
using PlayerRoles;
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
            Log.Info(
                $"Player [{player.PlayerId}] {player.Nickname} ({player.UserId}) joined the server from {player.IpAddress}");
        }

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(PlayerAPI player)
        {
            Log.Info(
                $"Player [{player.PlayerId}]  {player.Nickname} ({player.UserId}) left the server. IP: {player.IpAddress}");
        }

        [PluginEvent(ServerEventType.PlayerReport)]
        public void OnPlayerReport(PlayerAPI issuer, PlayerAPI target, string reason)
        {
            Log.Info(
                $"Player [{issuer.PlayerId}]  {issuer.Nickname} ({issuer.UserId}) reported [{target.PlayerId}]  {target.Nickname} ({target.UserId}): {reason}");

            Plugin.Reports.Send(issuer, target, reason);
        }

        [PluginEvent(ServerEventType.PlayerSpawn)]
        public void FixTutorialOnPlayerSpawn(PlayerAPI player, RoleTypeId role)
        {
            if (role is not RoleTypeId.Tutorial)
            {
                return;
            }

            // Не знаю присутствует этот "баг" всё ещё или нет
            Timing.CallDelayed(1f, () =>
            {
                // Админ быстро прокликал форс класс
                if (player.Role != role)
                {
                    return;
                }

                // Выдадим очень много HP вместо годмода. Эффект +/- одинаковый, только годмод в 12.0 может не убраться после форскласса
                player.Health = float.MaxValue;
                player.Position = Plugin.Configs.TowerPosition;
            });
        }
    }
}