namespace Padoru.OfflineBans.Commands
{
    using CommandSystem;
    using System;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class RecieverParent : ParentCommand
    {
        public RecieverParent() => LoadGeneratedCommands();

        public override string Command { get; } = "ofban";

        public override string[] Aliases { get; } = new string[] { "ofb", "of" };

        public override string Description { get; } = "Основа для офбанов";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Add());
            RegisterCommand(new Modify());
            RegisterCommand(new Delete());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Формат команды:\nofban (add/modify/del) (айди нарушителя) (срок) (причина)";
            return false;
        }
    }
}
