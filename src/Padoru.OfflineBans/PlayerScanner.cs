namespace Padoru.OffBans
{
    using Padoru.API.Features.Plugins;
    using Padoru.OffBans.Classes;
    using Padoru.OfflineBans.Classes;
    using PluginAPI.Core.Attributes;
    using PluginAPI.Enums;
    using PluginAPI.Helpers;
    using System;
    using System.IO;
    using PlayerAPI = PluginAPI.Core.Player;
    public class PlayerScanner : IEventHandler
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerAPI player)
        {

            if (File.Exists(WantedPath.filepath + $"\\{player.UserId}.json"))
            {
                WantedUser.Ban(player);
            }
        }
    }
}
