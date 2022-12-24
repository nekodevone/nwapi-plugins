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
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) присоединился к серверу.");
        }

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) вышел с сервера.");
        }

        [PluginEvent(ServerEventType.PlayerDamage)]
        public void OnPlayerDamage(PlayerAPI target, PlayerAPI attacker, DamageHandlerBase damageHandler)
        {
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

            Plugin.Sender.AddToQuene(
                $"Игрок {target.GetInfo()} получил {attackerDamageHandler.Damage} урона от игрока {attacker.GetInfo()}.");
        }

        [PluginEvent(ServerEventType.PlayerBanned)]
        public void OnPlayerBanned(PlayerAPI player, ICommandSender commandSender, string reason, long duration)
        {
            if (commandSender is not CommandSender sender)
            {
                return;
            }

            var expirationTime = DateTime.Now.AddSeconds(duration);

            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()}) был заблокирован администратором {sender.Nickname}({sender.SenderId})\nПричина - {reason}\nДата разбана - {expirationTime}");
        }

        [PluginEvent(ServerEventType.PlayerDropItem)]
        public void OnPlayerDropItem(PlayerAPI player, ItemBase item)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()}) выбросил предмет {item.ItemTypeId}.");
        }

        [PluginEvent(ServerEventType.PlayerSearchedPickup)]
        public void OnPlayerSearchedPickup(PlayerAPI player, ItemPickupBase pickup)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.GetInfo()}) подобрал предмет {pickup.NetworkInfo.ItemId}.");
        }

        [PluginEvent(ServerEventType.PlayerEscape)]
        public void OnPlayerEscape(PlayerAPI player, RoleTypeId newRoleType)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) сбежал и стал {newRoleType}.");
        }

        [PluginEvent(ServerEventType.PlayerEnterPocketDimension)]
        public void OnPlayerEnterPocketDimension(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) попал в карманное измерение.");
        }

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void OnPlayerChangeRole(PlayerAPI player, PlayerRoleBase oldRole, RoleTypeId newRole,
            RoleChangeReason reason)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) теперь {newRole}({reason}).");
        }

        [PluginEvent(ServerEventType.PlayerDeath)]
        public void OnPlayerDeath(PlayerAPI player, PlayerAPI attacker, DamageHandlerBase damageHandler)
        {
            if (attacker is null)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) был убит {attacker.GetInfo()}.");
        }

        [PluginEvent(ServerEventType.PlayerThrowProjectile)]
        public void OnPlayerThrowProjectile(PlayerAPI player, ThrowableItem item, float forceAmount,
            float upwardsFactor, Vector3 torque, Vector3 velocity)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) бросил {item.ItemTypeId}.");
        }

        [PluginEvent(ServerEventType.Scp939CreateAmnesticCloud)]
        public void OnScp939CreateAmnesticCloud(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) начал выпускать амнеизак.");
        }


        [PluginEvent(ServerEventType.PlayerOpenGenerator)]
        public void OnPlayerOpenGenerator(PlayerAPI player, Scp079Generator generator)
        {
            var room = RoomIdUtils.RoomAtPosition(generator.transform.position).Name;
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) открыл генератор в комнате {room}");
        }

        [PluginEvent(ServerEventType.PlayerCloseGenerator)]
        public void OnPlayerCloseGenerator(PlayerAPI player, Scp079Generator generator)
        {
            var room = RoomIdUtils.RoomAtPosition(generator.transform.position).Name;
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) закрыл генератор в комнате {room}");
        }

        [PluginEvent(ServerEventType.PlayerInteractScp330)]
        public void OnPlayerInteractScp330(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.GetInfo()}) применил SCP-330.");
        }
    }
}