using System;
using CommandSystem;

namespace Padoru.OfflineBan.Commands.Admin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Parent : ParentCommand
    {
        public Parent() => LoadGeneratedCommands();

        public override string Command => "ofban";

        public override string[] Aliases { get; } = { "ofb", "of" };

        public override string Description => "Основа для офбанов";

        public sealed override void LoadGeneratedCommands()
        {
            RegisterCommand(new Add());
            RegisterCommand(new Modify());
            RegisterCommand(new Delete());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            response = "Формат команды:\nofban (add/modify/del) (айди нарушителя) (срок) (причина)";
            return false;
        }
    }
}