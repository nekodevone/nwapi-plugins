using CommandSystem;
using Padoru.OfflineBans.Classes;
using System;
using System.Linq;

namespace Padoru.OfflineBans.Commands
{
    public class Add : ICommand
    {
        public string Command { get; } = "add";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = "Принимает офбан и заносит нарушителя и нужную информацию в нужное место.";

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

            string id = arguments.ElementAt(0);

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

            string bantimestring = arguments.ElementAt(1);

            if (!WantedUser.TimeFormatCheck(bantimestring))
            {
                response = "Ошибка в указании времени";
                return false;
            }

            string reason = string.Join(" ", arguments.Skip(2));
            (long, string) bantime = WantedUser.GetBanTime(bantimestring);

            WantedUser.Add(id, bantime.Item1, reason);

            response = $"Бан успешно сохранён:\nID нарушителя: {id}\nДлительность: {bantime.Item2}\nПричина: {reason}";
            return false;
        }
    }
}
