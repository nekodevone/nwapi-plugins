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
        public void OnScp914Activate(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()}) использовал SCP-914. Режим {Scp914Controller.Singleton.Network_knobSetting})");
        }

        [PluginEvent(ServerEventType.Scp914KnobChange)]
        public void OnScp914KnobChange(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()}) переключил режим SCP-914. Предыдущий режим {Scp914Controller.Singleton.Network_knobSetting}.");
        }
    }
}