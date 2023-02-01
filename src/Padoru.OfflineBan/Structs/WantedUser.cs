using System.IO;
using Padoru.OfflineBan.Utils;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.OfflineBan.Structs
{
    public class WantedUser
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userId"><see cref="UserId"/></param>
        /// <param name="duration"><see cref="Duration"/></param>
        /// <param name="reason"><see cref="Reason"/></param>
        public WantedUser(string userId, long duration, string reason)
        {
            UserId = userId;
            Duration = duration;
            Reason = reason;
        }

        /// <summary>
        /// Пустой конструктор для сериализации
        /// </summary>
        public WantedUser()
        {
        }

        /// <summary>
        /// Айди
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Длительность бана
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// Причина бана
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Пытается получить разыскиваемого игрока
        /// </summary>
        /// <param name="userId">Айди игрока</param>
        /// <param name="wantedUser">Данные бана</param>
        /// <returns>Ожидает ли игрок бана</returns>
        public static bool TryGet(string userId, out WantedUser wantedUser)
        {
            if (!Has(userId))
            {
                wantedUser = null;
                return false;
            }

            var json = File.ReadAllText(GetPath(userId));
            wantedUser = Utf8Json.JsonSerializer.Deserialize<WantedUser>(json);
            return true;
        }

        /// <summary>
        /// Проверяет наличие json-файла с айди игрока в папке розыска.
        /// </summary>
        /// <param name="userId">Айди игрока.</param>
        /// <returns>true если файл существует.</returns>
        public static bool Has(string userId)
        {
            return File.Exists(GetPath(userId));
        }

        /// <summary>
        /// В папке розыска создает json-файл розыска с именем-айди игрока и сериализует туда класс WantedUser.
        /// </summary>
        /// <param name="userId">Айди игрока.</param>
        /// <param name="time">Срок бана.</param>
        /// <param name="reason">Причина бана.</param>
        public static void Add(string userId, long time, string reason)
        {
            var user = new WantedUser(userId, time, reason);
            var json = Utf8Json.JsonSerializer.ToJsonString(user);

            File.WriteAllText(GetPath(userId), json);
        }

        /// <summary>
        /// Инструмент для получения пути к json-файлу игрока в розыске.
        /// </summary>
        /// <param name="userId">Стим или дискорд айди.</param>
        /// <returns>Путь к json-файлу игрока в розыске.</returns>
        private static string GetPath(string userId)
        {
            return Path.Combine(Tools.FolderPath, userId + ".json");
        }

        /// <summary>
        /// Банит игрока на срок и с причиной из json-файла, после чего удаляет этот файл.
        /// </summary>
        /// <param name="player">Игрок.</param>
        public void Ban(PlayerAPI player)
        {
            player.Ban(Reason, Duration);
            Delete();
        }

        /// <summary>
        /// Удаляет файл с баном
        /// </summary>
        public void Delete()
        {
            File.Delete(GetPath(UserId));
        }
    }
}