using Padoru.API.Features.Plugins;
using Padoru.Logger.Extensions;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using Scp914;
using PlayerAPI = PluginAPI.Core.Player;


namespace Padoru.Logger.Events.Internal
{
    public class Scp914 : IEventHandler
    {
        [PluginEvent(ServerEventType.Scp914Activate)]
        public void OnScp914Activate(PlayerAPI player, Scp914KnobSetting knobSetting)
        {
            if (!Plugin.Configs.LoggingEvents.Scp914Activate)
            {
                return;
            }

            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()}) использовал SCP-914 на режиме {knobSetting}.");
        }

        [PluginEvent(ServerEventType.Scp914KnobChange)]
        public void OnScp914KnobChange(PlayerAPI player, Scp914KnobSetting knobSetting,
            Scp914KnobSetting previousKnobSetting)
        {
            if (!Plugin.Configs.LoggingEvents.Scp914KnobChange)
            {
                return;
            }

            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()}) переключил режим SCP-914 с {previousKnobSetting} на {knobSetting}.");
        }
    }
}