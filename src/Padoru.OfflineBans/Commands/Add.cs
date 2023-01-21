namespace Padoru.OfflineBans.Commands
{
    using CommandSystem;
    using Padoru.OffBans.Classes;
    using Padoru.OfflineBans.Classes;
    using System;
    using System.IO;
    using System.Linq;

    public class Add : ParentCommand
    {
        public Add() => LoadGeneratedCommands();

        public override string Command { get; } = "add";

        public override string[] Aliases { get; }

        public override string Description { get; } = "Принимает офбан и заносит нарушителя и нужную информацию в нужное место.";

        public override void LoadGeneratedCommands() { }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
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

            if (!Directory.Exists(Tools.filepath))
            {
                Directory.CreateDirectory(Tools.filepath);
            }

            string id = arguments.ElementAt(0);
            string reason = string.Join(" ", arguments.Skip(2).ToArray());
            (long, string) bantime = Tools.GetBanTime(arguments.ElementAt(1));
            if (bantime.Item1 == -1L)
            {
                response = "Ошибка в указании времени";
                return false;
            }
            WantedUser user = new WantedUser(id, bantime.Item1, reason);
            string json = Utf8Json.JsonSerializer.ToJsonString(user);
            //string json = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText(Tools.filepath + $"\\{id}.json", json);

            response = $"Бан успешно сохранён:\nID нарушителя: {id}\nДлительность: {bantime.Item2}\nПричина: {reason}";
            return false;
        }
    }
}
