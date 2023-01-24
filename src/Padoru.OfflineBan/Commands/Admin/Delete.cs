using System;
using System.Linq;
using CommandSystem;
using Padoru.OfflineBan.Structs;
using Padoru.OfflineBan.Utils;

namespace Padoru.OfflineBan.Commands.Admin
{
    public class Delete : ICommand
    {
        public string Command => "delete";

        public string[] Aliases { get; } = { "del" };

        public string Description => "Удаляет офбан";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(PlayerPermissions.BanningUpToDay))
            {
                response = "Недостаточно прав";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Формат команды:\nofban delete (айди нарушителя)";
                return false;
            }

            var id = arguments.ElementAt(0);

            if (!Tools.IsIdValid(id))
            {
                response = "Неправильный ID игрока";
                return false;
            }

            if (!WantedUser.TryGet(id, out var wantedUser))
            {
                response = "Игрок не ожидает бана";
                return false;
            }

            wantedUser.Delete();

            response = "Ожидаемый бан удалён";
            return true;
        }
    }
}