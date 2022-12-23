using System;
using CommandSystem;
using NorthwoodLib.Pools;
using PluginAPI.Core;
using UnityEngine;
using Color = Padoru.API.Color;

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
            const int take = 7;

            var banList = BanHandler.GetBans(BanHandler.BanType.UserId);

            if (banList.IsEmpty())
            {
                response = $"<color={Color.Blue}>Список забаненных пуст</color>";
                return true;
            }

            var sb = StringBuilderPool.Shared.Rent();

            sb.AppendLine($"<color={Color.Blue}>Список забаненных игроков ({banList.Count}):</color>");
            sb.AppendLine("Для навигации используйте <b>bans [страница]</b>\n");

            var skip = arguments.Count > 0 && int.TryParse(arguments.At(0), out var page)
                ? Mathf.Clamp((page - 1) * take, 0, banList.Count - take)
                : 0;

            // bruh I'm tired asf
            for (var i = banList.Count - skip - 1; i >= banList.Count - skip - take; i--)
            {
                var ban = banList[i];

                var issuedAt = new DateTime(ban.IssuanceTime);
                var expiresAt = new DateTime(ban.Expires);

                var issuerId = GetIssuerId(ban.Issuer);
                var duration = GetDuration(issuedAt, expiresAt);

                sb.AppendLine(
                    $"[{i}. {issuedAt:dd.MM.yy HH:mm:ss}] <b>{issuerId}</b> забанил <b>{ban.Id}</b> на <b>{duration}</b>: {ban.Reason}"
                );
            }

            response = StringBuilderPool.Shared.ToStringReturn(sb);
            return true;
        }

        /// <summary>
        /// Возвращает в строке содержимое в скобках или строку полностью, если скобок нет
        /// </summary>
        private static string GetIssuerId(string issuer)
        {
            var index = issuer.IndexOf('(');

            return index == -1
                ? issuer
                : issuer.Substring(index + 1, issuer.Length - index - 2);
        }

        /// <summary>
        /// Возвращает длительность бана в формате "5 мин" / "1 час" / "1 дн" / "1 мес" / "1 год"
        /// </summary>
        private static string GetDuration(DateTime issuedAt, DateTime expiresAt)
        {
            var duration = expiresAt - issuedAt;

            if (duration.TotalMinutes < 60)
            {
                return $"{duration.Minutes}m";
            }

            if (duration.TotalHours < 24)
            {
                return $"{duration.Hours}h";
            }

            return duration.TotalDays switch
            {
                < 7 => $"{duration.Days}W",
                < 30 => $"{duration.Days}D",
                < 365 => $"{duration.Days / 30}M",
                _ => $"{duration.Days / 365}Y"
            };
        }
    }
}