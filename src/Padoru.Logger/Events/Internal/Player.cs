using System;
using CommandSystem;
using InventorySystem.Items;
using InventorySystem.Items.ThrowableProjectiles;
using Padoru.API.Features.Plugins;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core.Attributes;
using PluginAPI.Core.Zones.Heavy;
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

        [PluginEvent(ServerEventType.PlayerSearchedPickup)]
        public void OnPlayerSearchedPickup(PlayerAPI player, ItemBase item)
        {
            Plugin.Sender.AddToQuene(
                $"Игрок {player.Nickname}({player.UserId}) подобрал предмет {item.ItemTypeId}");
        }

        [PluginEvent(ServerEventType.PlayerEscape)]
        public void OnPlayerEscape(PlayerAPI player, RoleTypeId newRoleType)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) теперь {newRoleType}");
        }

        [PluginEvent(ServerEventType.PlayerEnterPocketDimension)]
        public void OnPlayerEnterPocketDimension(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) попал в карманное измерение");
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

        [PluginEvent(ServerEventType.RoundEnd)]
        public void OnRoundEnd(RoundSummary.LeadingTeam LeadTeam)
        {
            Plugin.Sender.AddToQuene($"Раунд закончился победой {LeadTeam}");
        }

        [PluginEvent(ServerEventType.RoundStart)]
        public void OnRoundStart()
        {
            Plugin.Sender.AddToQuene($"Раунд начался");
        }

        [PluginEvent(ServerEventType.PlayerThrowProjectile)]
        public void OnPlayerThrowProjectile(PlayerAPI player , ThrowableItem ThrItem)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) бросил {ThrItem}.");
        }
        
        [PluginEvent(ServerEventType.Scp914Activate)]
        public void OnScp914Activate(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) использовал SCP-914");
        } 
        
        [PluginEvent(ServerEventType.Scp914KnobChange)]
        public void OnScp914KnobChange(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) переключил режим SCP-914");
        } 
        
        [PluginEvent(ServerEventType.Scp939CreateAmnesticCloud)]
        public void OnScp939CreateAmnesticCloud(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) выпустил амнеизак за SCP-939");
        }

    
    [PluginEvent(ServerEventType.Scp079LevelUpTier)]
        public void OnScp079LevelUpTier(PlayerAPI player , int int079)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) повысил свой tier до {int079}");
        }

    [PluginEvent(ServerEventType.PlayerOpenGenerator)]
    public void OnPlayerOpenGenerator(PlayerAPI player , Generator gen)
    {
        Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) открыл генератор {gen}");
    }

        [PluginEvent(ServerEventType.PlayerCloseGenerator)]
        public void OnPlayerCloseGenerator(PlayerAPI player , Generator gen)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) закрыл генератор {gen}");
        } 
        
        [PluginEvent(ServerEventType.PlayerInteractScp330)]
        public void OnPlayerInteractScp330(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) применил SCP-330");
        }
        
        [PluginEvent(ServerEventType.WarheadStart)]
        public void OnWarheadStart(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) активировал процесс детонации Альфа-Боеголовки");
        }
        
        [PluginEvent(ServerEventType.WarheadStop)]
        public void OnWarheadStop(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) остановил процесс детонации Альфа-Боеголовки");
        }
        
        [PluginEvent(ServerEventType.WarheadDetonation)]
        public void OnWarheadDetonation(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) взорвал Альфа-Боеголовку");
        }
        
        [PluginEvent(ServerEventType.Scp106Stalking)]
        public void OnScp106Stalking(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) переключил stalk mode за SCP-106");
        }
        
        [PluginEvent(ServerEventType.Scp079UseTesla)]
        public void OnScp079UseTesla(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) активровал Tesla-ворота за SCP-079");
        }
        
        [PluginEvent(ServerEventType.Scp079LockdownRoom)]
        public void OnScp079LockdownRoom(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) заблокировал комнату за SCP-079");
        }
        
        [PluginEvent(ServerEventType.Scp079BlackoutZone)]
        public void OnScp079BlackoutZone(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) отключил питание в одной из зон комплекса");
        }
        
        [PluginEvent(ServerEventType.Scp049ResurrectBody)]
        public void OnScp049ResurrectBody(PlayerAPI player)
        {
            Plugin.Sender.AddToQuene($"Игрок {player.Nickname}({player.UserId}) воскресил тело за SCP-049");
        }
        
    }