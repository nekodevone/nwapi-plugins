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

        public void LoadGeneratedCommands() { }

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(PlayerPermissions.BanningUpToDay))
            {
                response = "Недостаточно прав";
                return false;
            }

            string clearString = string.Join(" ", arguments.Array);
            if (Tools.RegexDel[0].IsMatch(clearString) || Tools.RegexDel[1].IsMatch(clearString))
            {
                response = "Формат команды:\nofban (add/modify/del) (айди нарушителя) (срок) (причина)";
                return false;
            }

            string id = arguments.ElementAt(0);
            try
            {
                File.Delete(Tools.FolderPath + $"\\{id}.json");
                response = "Ожидаемый бан удалён";
                return true;
            }
            catch (Exception e)
            {
                response = "Бан не найден";
                return false;
            }
        }
    }
}
