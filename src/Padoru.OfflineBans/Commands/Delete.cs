namespace Padoru.OfflineBans.Commands
{
    using CommandSystem;
    using System;
    using System.IO;
    using System.Linq;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Delete : RecieverParent
    {
        public override string Command { get; } = "ofban del";

        public override string[] Aliases { get; } = new string[] { "ofb del", "offban del" };

        public override string Description { get; } = "Удаляет офбан.";

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
            try
            {
                File.Delete(filepath + $"\\{id}.json");
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
