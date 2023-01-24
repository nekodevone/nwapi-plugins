using CommandSystem;
using Padoru.OfflineBans.Classes;
using System;
using System.IO;
using System.Linq;

namespace Padoru.OfflineBans.Commands
{
    public class Delete : ICommand
    {
        public string Command { get; } = "del";

        public string[] Aliases { get; } = new string[] { "delete" };

        public string Description { get; } = "Удаляет офбан";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(PlayerPermissions.BanningUpToDay))
            {
                response = "Недостаточно прав";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Формат команды:\nofban del (айди нарушителя)";
                return false;
            }

            string id = arguments.ElementAt(0);

            if (!Tools.IsIdValid(id))
            {
                response = "Неправильный ID игрока";
                return false;
            }

            if (!WantedUser.Has(id))
            {
                response = "Бан не найден";
                return false;
            }

            File.Delete(Tools.GetPath(id));
            response = "Ожидаемый бан удалён";
            return true;
        }
    }
}
