using Padoru.API.Features.Plugins;
using Padoru.Logger.Extensions;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;


namespace Padoru.Logger.Events.Internal
{
    public class Scp106 : IEventHandler
    {
        [PluginEvent(ServerEventType.Scp106Stalking)]
        public void OnScp106Stalking(PlayerAPI player, bool activate)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()} {(activate ? "использовал сталк" : "вышел из сталка")}");
        }
    }
}