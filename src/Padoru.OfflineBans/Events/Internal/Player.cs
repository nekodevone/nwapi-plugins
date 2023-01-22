using Padoru.API.Features.Plugins;
using Padoru.OfflineBans.Classes;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using System.IO;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.OfflineBans.Events.Internal
{
    public class Player : IEventHandler
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerAPI player)
        {
            if (File.Exists(Tools.FolderPath + $"\\{player.UserId}.json"))
            {
                WantedUser.Ban(player);
            }
        }
    }
}
