using CommandSystem;
using HarmonyLib;
using Padoru.API.Features.Plugins;
using Padoru.Logger.Extensions;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Events.Internal
{
    public class Server : IEventHandler
    {
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void OnWaitingForPlayers()
        {
            Plugin.Sender.AddToQuene("Сервер ожидает игроков");
        }

        [PluginEvent(ServerEventType.RoundStart)]
        public void OnRoundStart()
        {
            Plugin.Sender.AddToQuene("Раунд начался");
        }

        [PluginEvent(ServerEventType.RoundEnd)]
        public void OnRoundEnd(RoundSummary.LeadingTeam leadTeam)
        {
            Plugin.Sender.AddToQuene($"Раунд закончился победой {leadTeam}");
        }

        [PluginEvent(ServerEventType.RoundRestart)]
        public void OnRoundRestart()
        {
            Plugin.Sender.AddToQuene("Раунд рестартится");
        }

        [PluginEvent(ServerEventType.RemoteAdminCommandExecuted)]
        public void OnRemoteAdminCommandExecuted(ICommandSender commandSender, string command, string[] arguments,
            bool result,
            string response)
        {
            if (commandSender is not CommandSender sender)
            {
                return;
            }

            Plugin.Sender.AddToQuene(
                $"Администратор {PlayerAPI.Get(sender.SenderId).GetInfo()} использовал команду {command} {arguments.Join(delimiter: " ")}");
        }
    }
}