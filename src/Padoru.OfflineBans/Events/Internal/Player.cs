using Padoru.API.Features.Plugins;
using Padoru.OfflineBans.Classes;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.OfflineBans.Events.Internal
{
    public class Player : IEventHandler
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerAPI player)
        {
            if (WantedUser.Has(player.UserId))
            {
                WantedUser.Ban(player);
            }
        }
    }
}
