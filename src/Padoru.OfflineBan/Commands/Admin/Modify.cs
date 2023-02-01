using System;
using System.Linq;
using CommandSystem;
using Padoru.OfflineBan.Structs;
using Padoru.OfflineBan.Utils;
using Padoru.OfflineBan.Extensions;

namespace Padoru.OfflineBan.Commands.Admin
{
    public class Modify : ICommand
    {
        public string Command => "modify";

        public string[] Aliases { get; } = { "edit", "mod" };

        public string Description => "Редактирует офбан, нарушителя и нужную информацию в нужном месте.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(PlayerPermissions.BanningUpToDay))
            {
                response = "Недостаточно прав";
                return false;
            }

            if (arguments.Count < 3)
            {
                response = "Формат команды:\nofban modify (айди нарушителя) (срок) (причина)";
                return false;
            }

            var id = arguments.ElementAt(0);

            if (!Tools.IsIdValid(id))
            {
                response = "Неправильный ID игрока";
                return false;
            }

            var rawTime = arguments.ElementAt(1);

            if (!rawTime.RelativeTimeToSeconds(":", out var time))
            {
                response = "Ошибка в указании времени";
                return false;
            }

            var reason = string.Join(" ", arguments.Skip(2));
            WantedUser.Add(id, time, reason);

            response = $"Бан успешно изменён:\nID нарушителя: {id}\nДлительность: {time}\nПричина: {reason}";
            return false;
        }
    }
}