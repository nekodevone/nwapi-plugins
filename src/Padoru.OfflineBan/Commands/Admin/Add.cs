using System;
using System.Linq;
using CommandSystem;
using Padoru.OfflineBan.Structs;
using Padoru.OfflineBan.Utils;
using Padoru.OfflineBan.Extensions;

namespace Padoru.OfflineBan.Commands.Admin
{
    public class Add : ICommand
    {
        public string Command => "add";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description => "Принимает офбан и заносит нарушителя и нужную информацию в нужное место.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(PlayerPermissions.BanningUpToDay))
            {
                response = "Недостаточно прав";
                return false;
            }

            if (arguments.Count < 3)
            {
                response = "Формат команды:\nofban add (айди нарушителя) (срок) (причина)";
                return false;
            }

            var id = arguments.ElementAt(0);

            if (!Tools.IsIdValid(id))
            {
                response = "Неправильный ID игрока";
                return false;
            }

            if (WantedUser.Has(id))
            {
                response = "Пользователь с таким ID уже находится в розыске. Используй modify или del";
                return false;
            }

            var rawTime = arguments.ElementAt(1);

            if (!rawTime.RelativeTimeToSeconds(out var time))
            {
                response = "Ошибка в указании времени";
                return false;
            }

            var reason = string.Join(" ", arguments.Skip(2));

            WantedUser.Add(id, time, reason);

            response = $"Бан успешно сохранён:\nID нарушителя: {id}\nДлительность: {time}\nПричина: {reason}";
            return false;
        }
    }
}