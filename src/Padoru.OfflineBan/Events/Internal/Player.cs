using Padoru.API.Features.Plugins;
using Padoru.OfflineBan.Structs;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.OfflineBan.Events.Internal
{
    public class Player : IEventHandler
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerAPI player)
        {
            if (!WantedUser.TryGet(player.UserId, out var wantedUser))
            {
                return;
            }

            wantedUser.Ban(player);
        }
    }
}