using System;
using CommandSystem;
using Padoru.API;
using PluginAPI.Core;

namespace Padoru.Kit.Commands.Admin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public sealed class Jail : ICommand
    {
        public string Command => "jail";

        public string Description => "Заключает игрока в башне или освобождает его";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var target = arguments.Count > 0 && int.TryParse(arguments.At(0), out var targetId)
                ? Player.Get(targetId)
                : Player.Get((sender as CommandSender)?.SenderId);

            if (target is null)
            {
                response = $"<color={Color.Red}>Игрок не найден</color>";
                return false;
            }

            if (Plugin.Jail.IsJailed(target))
            {
                Plugin.Jail.Release(target);

                response = $"<color={Color.Green}>Игрок [{target.PlayerId}] {target.Nickname} освобожден</color>";
                return true;
            }

            Plugin.Jail.Arrest(target);

            response = $"<color={Color.Green}>Игрок [{target.PlayerId}] {target.Nickname} заключен</color>";
            return true;
        }
    }
}