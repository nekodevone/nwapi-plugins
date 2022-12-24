using MapGeneration;
using Padoru.API.Features.Plugins;
using Padoru.Logger.Extensions;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Events.Internal
{
    public class Scp079 : IEventHandler
    {
        [PluginEvent(ServerEventType.Scp079UseTesla)]
        public void OnScp079UseTesla(PlayerAPI player, TeslaGate teslaGate)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} активировал Тесла-ворота.");
        }

        [PluginEvent(ServerEventType.Scp079LockdownRoom)]
        public void OnScp079LockdownRoom(PlayerAPI player, RoomIdentifier room)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} заблокировал комнату {room.Name}.");
        }

        [PluginEvent(ServerEventType.Scp079BlackoutZone)]
        public void OnScp079BlackoutZone(PlayerAPI player, FacilityZone zone)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()} отключил питание в {zone} зоне.");
        }

        [PluginEvent(ServerEventType.Scp079LevelUpTier)]
        public void OnScp079LevelUpTier(PlayerAPI player, int tier)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} повысил свой уровень до {tier}.");
        }
    }
}