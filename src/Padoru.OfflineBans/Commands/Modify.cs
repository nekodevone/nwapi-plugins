namespace Padoru.OfflineBans.Commands
{
    using CommandSystem;
    using Newtonsoft.Json;
    using Padoru.OffBans.Classes;
    using Padoru.OfflineBans.Classes;
    using System;
    using System.IO;
    using System.Linq;
    using Formatting = Newtonsoft.Json.Formatting;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Modify : ParentCommand
    {
        public Modify() => LoadGeneratedCommands();

        public override string Command { get; } = "modify";

        public override string[] Aliases { get; } = new string[] { "edit", "mod" };

        public override string Description { get; } = "Редактирует офбан, нарушителя и нужную информацию в нужном месте.";

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
            WantedUser user = new WantedUser(id, bantime.Item1, reason);
            string json = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText(Tools.filepath + $"\\{id}.json", json);

            response = $"Бан успешно изменён:\nID нарушителя: {id}\nДлительность: {bantime.Item2}.\nПричина: {reason}";
            return false;
        }
    }
}
