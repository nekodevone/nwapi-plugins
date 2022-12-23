using System;
using CommandSystem;
using NorthwoodLib.Pools;
using Padoru.API;

namespace Padoru.Kit.Commands.Admin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public sealed class BanList : ICommand
    {
        public string Command => "banlist";

        public string Description => "Список забаненных игроков";

        public string[] Aliases { get; } = { "bans" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            const int take = 10;

            var banList = BanHandler.GetBans(BanHandler.BanType.UserId);

            if (banList.IsEmpty())
            {
                response = $"<color={Color.Blue}>Список забаненных пуст</color>";
                return true;
            }

            var sb = StringBuilderPool.Shared.Rent();

            sb.AppendLine($"<color={Color.Blue}>Список забаненных игроков ({banList.Count}):</color>");
            sb.AppendLine("<i>для постраничной навигации используйте <b>bans [page]</b></i>\n");

            var skip = arguments.Count > 0 && int.TryParse(arguments.At(0), out var page)
                ? (page - 1) * take
                : 0;

            banList.Reverse();

            for (var i = skip; i < take; i++)
            {
                var ban = banList[i];
                var number = banList.Count - i;

                var issuedAt = new DateTime(ban.IssuanceTime);
                var expiresAt = new DateTime(ban.Expires);

                var duration = GetDuration(issuedAt, expiresAt);

                sb.AppendLine(
                    $"<b>{number} {issuedAt:dd.MM.yy HH:mm:ss}.</b> <b>{ban.Issuer}</b> забанил(-а) <b>{ban.Id}</b> на <b>{duration}</b>: {ban.Reason}"
                );
            }

            response = sb.ToString();
            return true;
        }

        /// <summary>
        /// Возвращает длительность бана в формате "5 мин" / "1 час" / "1 дн" / "1 мес" / "1 год"
        /// </summary>
        /// <param name="issuedAt"></param>
        /// <param name="expiresAt"></param>
        /// <returns></returns>
        private static string GetDuration(DateTime issuedAt, DateTime expiresAt)
        {
            var duration = expiresAt - issuedAt;

            if (duration.TotalMinutes < 60)
            {
                return $"{duration.Minutes} мин";
            }

            if (duration.TotalHours < 24)
            {
                return $"{duration.Hours} час";
            }

            return duration.TotalDays switch
            {
                < 30 => $"{duration.Days} дн",
                < 365 => $"{duration.Days / 30} мес",
                _ => $"{duration.Days / 365} лет"
            };
        }
    }
}