using PluginAPI.Helpers;
using System.IO;
using System.Text.RegularExpressions;

namespace Padoru.OfflineBans.Classes
{
    public static class Tools
    {
        public static string DirectoryName { get; } = "Wanted";

        public static string FolderPath { get; } = Path.Combine(Paths.PluginAPI, DirectoryName);

        public static Regex[] FormatRegex { get; } = { new Regex(@"\d{17}@steam"),
                                          new Regex(@"\d{18}@discord")};

        /// <summary>
        /// Инструмент для проверки соответствия айдишника дискордовскому или стимовскому паттерну.
        /// </summary>
        /// <param name="userId">Стим или дискорд айди.</param>
        /// <returns>true если переданный айди удовлетворяет одному из условий</returns>
        public static bool IsIdValid(string userId)
        {
            return FormatRegex[0].IsMatch(userId) || FormatRegex[1].IsMatch(userId);
        }

        /// <summary>
        /// Инструмент для получения пути к json-файлу игрока в розыске.
        /// </summary>
        /// <param name="userId">Стим или дискорд айди.</param>
        /// <returns>Путь к json-файлу игрока в розыске.</returns>
        public static string GetPath(string userId)
        {
            return Path.Combine(FolderPath, userId + ".json");
        }
    }
}
