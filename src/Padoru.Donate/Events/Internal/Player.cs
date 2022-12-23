using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Donate.Events.Internal
{
    public class Player
    {
        [PluginEvent(ServerEventType.PlayerCheckReservedSlot)]
        public bool PassOnPlayerCheckReservedSlot(string userId, bool hasReservedSlot)
        {
            return hasReservedSlot || false;
        }
    }
}