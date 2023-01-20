namespace Padoru.OfflineBans.Commands
{
    using CommandSystem;
    using Newtonsoft.Json;
    using Padoru.OffBans.Classes;
    using System;
    using System.IO;
    using System.Linq;
    using Formatting = Newtonsoft.Json.Formatting;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Modify : RecieverParent
    {
        public override string Command { get; } = "ofban modify";

        public override string[] Aliases { get; } = new string[] { "ofb modify", "offban modify" };

        public override string Description { get; } = "Редактирует офбан.";

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string clearString = string.Join(" ", arguments.Array);
            if (Regex[0].IsMatch(clearString) || Regex[1].IsMatch(clearString))
            {
                response = "Формат команды:\nofban (add/modify/del) (айди нарушителя) (срок) (причина)";
                return false;
            }

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string id = arguments.ElementAt(0);
            string reason = string.Join(" ", arguments.Skip(2).ToArray());
            (long, string) bantime = GetBanTime(arguments.ElementAt(1));
            WantedUser user = new WantedUser(id, bantime.Item1, reason);
            string json = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText(filepath + $"\\{id}.json", json);

            response = $"Бан успешно изменён:\nID нарушителя: {id}\nДлительность: {bantime.Item2}.\nПричина: {reason}";
            return false;
        }
    }
}
