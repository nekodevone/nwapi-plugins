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

            string clearString = string.Join(" ", arguments.Array);
            if (Tools.Regex[0].IsMatch(clearString) || Tools.Regex[1].IsMatch(clearString))
            {
                response = "Формат команды:\nofban (add/modify/del) (айди нарушителя) (срок) (причина)";
                return false;
            }

            if (File.Exists(Tools.FolderPath + $"{arguments.ElementAt(0)}.json"))
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
            string reason = string.Join(" ", arguments.Skip(2).ToArray());
            (long, string) bantime = WantedUser.GetBanTime(arguments.ElementAt(1));

            WantedUser user = new WantedUser(id, bantime.Item1, reason);
            string json = Utf8Json.JsonSerializer.ToJsonString(user);
            File.WriteAllText(Tools.FolderPath + $"\\{id}.json", json);

            response = $"Бан успешно сохранён:\nID нарушителя: {id}\nДлительность: {bantime.Item2}\nПричина: {reason}";
            return false;
        }
    }
}
