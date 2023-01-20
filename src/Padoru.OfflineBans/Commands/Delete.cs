namespace Padoru.OfflineBans.Commands
{
    using CommandSystem;
    using Padoru.OfflineBans.Classes;
    using System;
    using System.IO;
    using System.Linq;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Delete : ParentCommand
    {
        public Delete() => LoadGeneratedCommands();

        public override string Command { get; } = "del";

        public override string[] Aliases { get; } = new string[] { "delete" };

        public override string Description { get; } = "Удаляет офбан";

        public override void LoadGeneratedCommands() { }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(PlayerPermissions.BanningUpToDay))
            {
                response = "Недостаточно прав";
                return false;
            }

            string clearString = string.Join(" ", arguments.Array);
            if (Tools.Regex[2].IsMatch(clearString) || Tools.Regex[3].IsMatch(clearString))
            {
                response = "Формат команды:\nofban (add/modify/del) (айди нарушителя) (срок) (причина)";
                return false;
            }

            if (!Directory.Exists(Tools.filepath))
            {
                Directory.CreateDirectory(Tools.filepath);
            }

            string id = arguments.ElementAt(0);
            try
            {
                File.Delete(Tools.filepath + $"\\{id}.json");
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
