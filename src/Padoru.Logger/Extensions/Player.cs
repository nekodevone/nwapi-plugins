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
            return $"{player.PlayerId} - {player.Nickname} ({player.UserId})[{player.Role}]";
        }
    }
}