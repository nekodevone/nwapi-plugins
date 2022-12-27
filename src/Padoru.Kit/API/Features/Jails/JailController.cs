using System;
using System.Collections.Generic;
using InventorySystem;
using MEC;
using Padoru.API;
using PlayerRoles;
using PluginAPI.Core;

namespace Padoru.Kit.API.Features.Jails
{
    /// <summary>
    /// Контроллер тюрьмы
    /// </summary>
    public class JailController
    {
        /// <summary>
        /// Снимки заключённых игроков
        /// </summary>
        public Dictionary<Player, PlayerSnapshot> Snapshots { get; } = new();

        /// <summary>
        /// Проверяет, находится ли игрок в тюрьме
        /// </summary>
        public bool IsJailed(Player player)
        {
            return Snapshots.ContainsKey(player);
        }

        /// <summary>
        /// Арестовать игрока
        /// </summary>
        public void Arrest(Player player)
        {
            if (Snapshots.ContainsKey(player))
            {
                return;
            }

            Snapshots[player] = new PlayerSnapshot(player);

            player.SetRole(RoleTypeId.Tutorial);

            if (player.RemoteAdminAccess)
            {
                return;
            }

            try
            {
                player.SendBroadcast(
                    $"<color={Color.Red}>Администратор вызвал вас на разборки. Не выходите из сервера.</color>",
                    15,
                    shouldClearPrevious: true
                );
            }
            catch (Exception error)
            {
                Log.Error($"Hubert wants us to ignore this error: {error}");
            }
        }

        /// <summary>
        /// Выпустить игрока
        /// </summary>
        public void Release(Player player)
        {
            if (!Snapshots.TryGetValue(player, out var snapshot))
            {
                return;
            }

            Snapshots.Remove(player);

            Timing.RunCoroutine(RestoreSnapshot(player, snapshot));
        }

        /// <summary>
        /// Очищает список заключённых
        /// </summary>
        /// <param name="release">Выпустить ли игроков из тюрьмы перед очищением</param>
        public void Clear(bool release = false)
        {
            if (release)
            {
                foreach (var player in Snapshots.Keys)
                {
                    Release(player);
                }
            }

            Snapshots.Clear();
        }

        /// <summary>
        /// Восстановить свойства игрока
        /// </summary>
        private static IEnumerator<float> RestoreSnapshot(Player player, PlayerSnapshot snapshot)
        {
            // Установка роли
            player.SetRole(snapshot.Role);

            yield return Timing.WaitForSeconds(0.3f);

            // Очистка инвентаря
            player.Position = snapshot.Position;
            player.Rotation = snapshot.Rotation;
            player.Health = snapshot.Health;

            var inventory = player.GetInventory();

            foreach (var item in inventory.UserInventory.Items.Values)
            {
                inventory.ServerRemoveItem(item.ItemSerial, null);
            }

            yield return Timing.WaitForSeconds(0.3f);

            // Установка инвентаря
            foreach (var item in snapshot.Items)
            {
                inventory.ServerAddItem(item);
            }

            foreach (var ammo in snapshot.Ammo)
            {
                player.AddAmmo(ammo.Key, ammo.Value);
            }
        }
    }
}