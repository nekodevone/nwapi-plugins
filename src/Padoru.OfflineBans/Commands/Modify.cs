using CommandSystem;
using Padoru.OfflineBans.Classes;
using System;
using System.IO;
using System.Linq;

namespace Padoru.OfflineBans.Commands
{
    public class Modify : ICommand
    {
        public string Command { get; } = "modify";

        public string[] Aliases { get; } = new string[] { "edit", "mod" };

        public string Description { get; } = "Редактирует офбан, нарушителя и нужную информацию в нужном месте.";

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

            string id = arguments.ElementAt(0);
            string reason = string.Join(" ", arguments.Skip(2));
            (long, string) bantime = WantedUser.GetBanTime(arguments.ElementAt(1));

            WantedUser.Add(id, bantime.Item1, reason);

            response = $"Бан успешно изменён:\nID нарушителя: {id}\nДлительность: {bantime.Item2}\nПричина: {reason}";
            return false;
        }
    }
}
