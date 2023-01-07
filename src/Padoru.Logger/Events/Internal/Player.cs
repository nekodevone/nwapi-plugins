using System;
using CommandSystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using MapGeneration;
using MapGeneration.Distributors;
using Padoru.API.Features.Plugins;
using Padoru.Logger.Extensions;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using UnityEngine;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Events.Internal
{
    public class Player : IEventHandler
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerAPI player)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerJoined)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} присоединился к серверу.");
        }

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(PlayerAPI player)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerLeft)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} вышел с сервера.");
        }

        [PluginEvent(ServerEventType.PlayerDamage)]
        public void OnPlayerDamage(PlayerAPI target, PlayerAPI attacker, DamageHandlerBase damageHandler)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerDamage)
            {
                return;
            }

            if (attacker is null || damageHandler is not AttackerDamageHandler attackerDamageHandler)
            {
                return;
            }

            if (target == attacker)
            {
                Plugin.Sender.AddToQuene(
                    $"Игрок {target.GetInfo()} нанес {attackerDamageHandler.Damage} урона самому себе.");
                return;
            }

            if (attackerDamageHandler.IsFriendlyFire)
            {
                Plugin.Sender.AddToQuene(
                    $"Игрок {target.GetInfo()} получил {attackerDamageHandler.Damage} урона от союзника {attacker.GetInfo()}.");
            }
        }

        [PluginEvent(ServerEventType.PlayerBanned)]
        public void OnPlayerBanned(PlayerAPI player, ICommandSender commandSender, string reason, long duration)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerBanned || commandSender is not CommandSender sender)
            {
                return;
            }

            var expirationTime = DateTime.Now.AddSeconds(duration);

            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()} был заблокирован администратором {sender.Nickname}({sender.SenderId})\nПричина - {reason}\nДата разбана - {expirationTime}");
        }

        [PluginEvent(ServerEventType.PlayerDropItem)]
        public void OnPlayerDropItem(PlayerAPI player, ItemBase item)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerDropItem)
            {
                return;
            }

            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()} выбросил предмет {item.ItemTypeId}.");
        }

        [PluginEvent(ServerEventType.PlayerSearchedPickup)]
        public void OnPlayerSearchedPickup(PlayerAPI player, ItemPickupBase pickup)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerSearchedPickup)
            {
                return;
            }

            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()} подобрал предмет {pickup.NetworkInfo.ItemId}.");
        }

        [PluginEvent(ServerEventType.PlayerEscape)]
        public void OnPlayerEscape(PlayerAPI player, RoleTypeId newRoleType)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerEscape)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} сбежал и стал {newRoleType}.");
        }

        [PluginEvent(ServerEventType.PlayerEnterPocketDimension)]
        public void OnPlayerEnterPocketDimension(PlayerAPI player)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerEnterPocketDimension)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} попал в карманное измерение.");
        }

        [PluginEvent(ServerEventType.PlayerExitPocketDimension)]
        public void OnPlayerExitPocketDimension(PlayerAPI player, bool isSuccessful)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerExitPocketDimension)
            {
                return;
            }

            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()} {(isSuccessful ? "выбрался из ки" : "выбрал не правильный выход из ки и умер")}.");
        }

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void OnPlayerChangeRole(PlayerAPI player, PlayerRoleBase oldRole, RoleTypeId newRole,
            RoleChangeReason reason)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerChangeRole)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} теперь {newRole}({reason}).");
        }

        [PluginEvent(ServerEventType.PlayerDeath)]
        public void OnPlayerDeath(PlayerAPI player, PlayerAPI attacker, DamageHandlerBase damageHandler)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerDeath || attacker is null)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} был убит {attacker.GetInfo()}.");
        }

        [PluginEvent(ServerEventType.PlayerThrowProjectile)]
        public void OnPlayerThrowProjectile(PlayerAPI player, ThrowableItem item,
            ThrowableItem.ProjectileSettings projectileSettings, bool fullForce)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerThrowProjectile)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} бросил {item.ItemTypeId}.");
        }

        [PluginEvent(ServerEventType.Scp939CreateAmnesticCloud)]
        public void OnScp939CreateAmnesticCloud(PlayerAPI player)
        {
            if (!Plugin.Configs.LoggingEvents.Scp939CreateAmnesticCloud)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} начал выпускать амнезиак.");
        }


        [PluginEvent(ServerEventType.PlayerOpenGenerator)]
        public void OnPlayerOpenGenerator(PlayerAPI player, Scp079Generator generator)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerOpenGenerator)
            {
                return;
            }

            var room = RoomIdUtils.RoomAtPosition(generator.transform.position).Name;
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} открыл генератор в комнате {room}");
        }

        [PluginEvent(ServerEventType.PlayerCloseGenerator)]
        public void OnPlayerCloseGenerator(PlayerAPI player, Scp079Generator generator)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerCloseGenerator)
            {
                return;
            }

            var room = RoomIdUtils.RoomAtPosition(generator.transform.position).Name;
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} закрыл генератор в комнате {room}");
        }

        [PluginEvent(ServerEventType.PlayerInteractScp330)]
        public void OnPlayerInteractScp330(PlayerAPI player)
        {
            if (!Plugin.Configs.LoggingEvents.PlayerInteractScp330)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()} применил SCP-330.");
        }
    }
}