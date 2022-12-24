using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using PluginAPI.Core;

namespace Padoru.API
{
    public static partial class Extensions
    {
        /// <summary>
        /// Возвращает <see cref="Inventory"/> игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns>Инвентарь игрока</returns>
        public static Inventory GetInventory(this Player player)
        {
            return player.ReferenceHub.inventory;
        }

        /// <summary>
        /// Возвращает <see cref="InventoryInfo"/> игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns>Текущий инвентарь игрока</returns>
        public static InventoryInfo GetCurrentInventory(this Player player)
        {
            return player.ReferenceHub.inventory.UserInventory;
        }

        /// <summary>
        /// Возвращает список предметов игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns>Список предметов игрока</returns>
        public static IEnumerable<ItemType> EnumerateItemList(this Player player)
        {
            return GetCurrentInventory(player).Items.Values.Select(item => item.ItemTypeId);
        }

        /// <summary>
        /// Возвращает список предметов игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns>Список предметов игрока</returns>
        public static ItemType[] GetItemList(this Player player)
        {
            return EnumerateItemList(player).ToArray();
        }

        /// <summary>
        /// Возвращает список патронов игрока и их количество
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns>Список патронов игрока и их количество</returns>
        public static Dictionary<ItemType, ushort> GetAmmoList(this Player player)
        {
            return new Dictionary<ItemType, ushort>(GetCurrentInventory(player).ReserveAmmo);
        }
    }
}