using System.Collections.Generic;
using System.Text.RegularExpressions;
using PlayerRoles;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Extensions
{
    public static class Player
    {
        private static readonly Dictionary<string, string> EscapeCache = new(1000);
        private static readonly Regex EscapePattern = new("(\\\\|-|_|\\*|~|`|@|\\|)", RegexOptions.Compiled);
        
        /// <summary>
        /// Получить подробную информацию о игроке
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns></returns>
        public static string GetInfo(this PlayerAPI player)
        {
            return $"**{player.PlayerId} - {player.Nickname.Escape()} ({player.UserId})[{player.Role}]**";
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
        
        /// <summary>
        /// Экранирует Markdown символы
        /// </summary>
        /// <param name="input">Текст</param>
        /// <returns>Экранированный текст</returns>
        public static string Escape(this string input)
        {
            if (!EscapeCache.TryGetValue(input, out var output))
            {
                output = EscapePattern.Replace(input, match => $"\\{match.Value}");
                EscapeCache[input] = output;
            }

            return output;
        }
    }
}