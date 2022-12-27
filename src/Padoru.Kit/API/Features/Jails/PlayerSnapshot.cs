using System.Collections.Generic;
using Padoru.API;
using PlayerRoles;
using PluginAPI.Core;
using UnityEngine;

namespace Padoru.Kit.API.Features.Jails
{
    /// <summary>
    /// Снимок игрока
    /// </summary>
    public class PlayerSnapshot
    {
        /// <summary>
        /// Игрок
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Здоровье игрока
        /// </summary>
        public float Health { get; set; }

        /// <summary>
        /// Роль игрока
        /// </summary>
        public RoleTypeId Role { get; set; }

        /// <summary>
        /// Позиция игрока
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Поворот игрока
        /// </summary>
        public Vector3 Rotation { get; set; }

        /// <summary>
        /// Список предметов игрока
        /// </summary>
        public ItemType[] Items { get; set; }

        /// <summary>
        /// Список патронов игрока и их количество
        /// </summary>
        public Dictionary<ItemType, ushort> Ammo { get; set; }

        /// <summary>
        /// Инстанциирует <see cref="PlayerSnapshot"/>
        /// </summary>
        public PlayerSnapshot()
        {
        }

        /// <summary>
        /// Инстанциирует <see cref="PlayerSnapshot"/> и делает снимок текущих параметров игрока
        /// </summary>
        /// <param name="player"></param>
        public PlayerSnapshot(Player player)
        {
            Player = player;
            Health = player.Health;
            Role = player.Role;
            Position = player.Position;
            Rotation = player.Rotation;
            Items = player.GetItemList();
            Ammo = player.GetAmmoList();
        }
    }
}