using System.Collections.Generic;
using System.Linq;
using NorthwoodLib.Pools;
using Padoru.API;
using PluginAPI.Core;

namespace Padoru.Kit.API.Features.Reports
{
    /// <summary>
    /// Контроллер репортов
    /// </summary>
    public class ReportsController
    {
        /// <summary>
        /// Список администрации
        /// </summary>
        public static IEnumerable<Player> StaffList => Player.GetPlayers().Where(player => player.RemoteAdminAccess);

        /// <summary>
        /// Список репортов
        /// </summary>
        public List<Report> List { get; } = new();

        /// <summary>
        /// Отправляет репорт на игрока от игрока администрации сервера
        /// </summary>
        /// <param name="issuer">Отправитель</param>
        /// <param name="target">Цель</param>
        /// <param name="reason">Причина репорта</param>
        public void Send(Player issuer, Player target, string reason)
        {
            var admins = ListPool<Player>.Shared.Rent(StaffList);
            var report = new Report(issuer, target, reason);

            List.Add(report);

            var text =
                $"<color={Color.Orange}><size=36>Репорт от <color={Color.Red}>[{issuer.PlayerId}] {issuer.Nickname}</color> на <color={Color.Red}>[{target.PlayerId}] {target.Nickname}</color></size></color>:<br><size=28>{reason}</size>";

            foreach (var admin in admins)
            {
                admin.SendBroadcast(text, 5);
            }

            ListPool<Player>.Shared.Return(admins);
        }
    }
}