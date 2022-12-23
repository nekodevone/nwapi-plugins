using System;
using CommandSystem;
using InventorySystem.Items;
using Padoru.API.Features.Plugins;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Events.Internal
{
    public class Player : IEventHandler
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) присоединился к серверу");
        }

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) вышел с сервера");
        }

        [PluginEvent(ServerEventType.PlayerDamage)]
        public void OnPlayerDamage(PlayerAPI target, PlayerAPI attacker, DamageHandlerBase damageHandler)
        {
            if (attacker is null)
            {
                return;
            }

            if (damageHandler is FirearmDamageHandler handler)
            {
                Plugin.Sender.AddToQuene(
                    $"Игрок {target.Nickname}({target.UserId}) нанес {handler.Damage} урона с помощью {handler.WeaponType}");
            }
        }

        [PluginEvent(ServerEventType.PlayerBanned)]
        public void OnPlayerBanned(PlayerAPI player, ICommandSender commandSender, string reason, long duration)
        {
            var sender = commandSender as CommandSender;
            var expirationTime = DateTime.Now.AddSeconds(duration);

            Plugin.Sender.AddToQuene(
                $"Игрок {player.Nickname}({player.UserId}) был забанен администратором {sender.Nickname}({sender.SenderId})\nПричина - {reason}\nДата разбана - {expirationTime}");
        }

        [PluginEvent(ServerEventType.PlayerDropItem)]
        public void OnPlayerDropItem(PlayerAPI player, ItemBase item)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.Nickname}({player.UserId}) выбросил предмет {item.ItemTypeId}");
        }

        [PluginEvent(ServerEventType.PlayerEscape)]
        public void OnPlayerEscape(PlayerAPI player, RoleTypeId newRoleType)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) теперь {newRoleType}");
        }

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void OnPlayerChangeRole(PlayerAPI player, PlayerRoleBase oldRole, RoleTypeId newRole,
            RoleChangeReason reason)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) теперь {newRole}({reason})");
        }

        [PluginEvent(ServerEventType.PlayerDeath)]
        public void OnPlayerDeath(PlayerAPI player, PlayerAPI attacker, DamageHandlerBase damageHandler)
        {
            if (attacker is null)
            {
                return;
            }

            Plugin.Sender.AddToQuene(
                $"Игрок {player.Nickname}({player.UserId}) был убит {attacker.Nickname}({attacker.UserId} - {attacker.Role})");
        }
    }
}