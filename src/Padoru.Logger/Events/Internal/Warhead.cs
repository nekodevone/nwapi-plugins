using Padoru.API.Features.Plugins;
using Padoru.Logger.Extensions;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Events.Internal
{
    public class Warhead : IEventHandler
    {
        [PluginEvent(ServerEventType.WarheadStart)]
        public void OnWarheadStart(bool isAutomatic, PlayerAPI player, bool isResumed)
        {
            if (!Plugin.Configs.LoggingEvents.WarheadStart || player is null)
            {
                return;
            }

            Plugin.Sender.AddToQuene(isAutomatic
                ? "Альфа-Боеголовка была активирована автоматически."
                : $"Игрок {player.GetInfo()} активировал процесс детонации Альфа-Боеголовки.");
        }

        [PluginEvent(ServerEventType.WarheadStop)]
        public void OnWarheadStop(PlayerAPI player)
        {
            if (!Plugin.Configs.LoggingEvents.WarheadStop || player is null)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} остановил процесс детонации Альфа-Боеголовки.");
        }

        [PluginEvent(ServerEventType.WarheadDetonation)]
        public void OnWarheadDetonation()
        {
            if (!Plugin.Configs.LoggingEvents.WarheadDetonation)
            {
                return;
            }

            Plugin.Sender.AddToQuene("Альфа-Боеголовка была взорвана.");
        }
    }
}