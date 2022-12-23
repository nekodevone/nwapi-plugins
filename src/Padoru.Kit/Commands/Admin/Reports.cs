using System;
using CommandSystem;
using NorthwoodLib.Pools;
using Padoru.API;

namespace Padoru.Kit.Commands.Admin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public sealed class Reports : ICommand
    {
        public string Command => "reports";

        public string Description => "Выводит список репортов за этот раунд";

        public string[] Aliases { get; } = { "репорты" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var sb = StringBuilderPool.Shared.Rent();
            var i = 0;

            foreach (var report in Plugin.Reports.List)
            {
                sb.AppendLine(
                    $"<color={Color.Orange}>#{++i} {GetElapsedTime(report.Date)} назад. <color={Color.Red}>[{report.IssuerId}] {report.IssuerName}</color> на <color={Color.Red}>[{report.TargetId}] {report.TargetName}</color>:</color> {report.Reason}");
            }

            response = StringBuilderPool.Shared.ToStringReturn(sb);
            return true;
        }

        /// <summary>
        /// Возвращает прошедшее время в формате "чч:мм:сс"
        /// </summary>
        /// <param name="time">Время отправки репорта</param>
        /// <returns>Время в формате "чч:мм:сс"</returns>
        private static string GetElapsedTime(DateTime time)
        {
            var elapsed = DateTime.Now - time;

            return $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}";
        }
    }
}