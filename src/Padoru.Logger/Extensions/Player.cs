using PlayerRoles;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Extensions
{
    public static class Player
    {
        /// <summary>
        /// Получить подробную информацию о игроке
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns></returns>
        public static string GetInfo(this PlayerAPI player)
        {
            return $"**{player.PlayerId} - {player.Nickname} ({player.UserId})[{player.Role}]**";
        }

        /// <summary>
        /// Получить <see cref="Team"/> игрока
        /// </summary>
        /// <param name="player">Игрока</param>
        /// <returns><see cref="Team"/></returns>
        public static Team GetTeam(this PlayerAPI player)
        {
            return player.ReferenceHub.roleManager.CurrentRole.Team;
        }
    }
}