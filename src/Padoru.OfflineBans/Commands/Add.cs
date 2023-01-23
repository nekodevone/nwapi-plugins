using CommandSystem;
using Padoru.OfflineBans.Classes;
using System;
using System.IO;
using System.Linq;

namespace Padoru.OfflineBans.Commands
{
    public class Add : ICommand
    {
        public string Command { get; } = "add";

        public string[] Aliases { get; }

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
                response = "Формат команды:\nofban (add/modify/del) (айди нарушителя) (срок) (причина)";
                return false;
            }

            if (!Tools.IsIdValid(arguments.ElementAt(0)))
            {
                response = "Неправильный ID игрока";
                return false;
            }

            if (WantedUser.Has(arguments.ElementAt(0)))
            {
                response = "Пользователь с таким ID уже находится в розыске. Используй modify или del";
                return false;
            }

            if (!WantedUser.TimeFormatCheck(arguments.ElementAt(1)))
            {
                response = "Ошибка в указании времени";
                return false;
            }

            string id = arguments.ElementAt(0);
            string reason = string.Join(" ", arguments.Skip(2));
            (long, string) bantime = WantedUser.GetBanTime(arguments.ElementAt(1));

            WantedUser.Add(id, bantime.Item1, reason);

            response = $"Бан успешно сохранён:\nID нарушителя: {id}\nДлительность: {bantime.Item2}\nПричина: {reason}";
            return false;
        }
    }
}
