using System.IO;
using System.Text.RegularExpressions;
using PluginAPI.Helpers;

namespace Padoru.OfflineBan.Utils
{
    public static class Tools
    {
        public static string DirectoryName => "Wanted";

        public static string FolderPath { get; } = Path.Combine(Paths.PluginAPI, DirectoryName);

        public static Regex[] FormatRegex { get; } =
        {
            new(@"\d{17}@steam"),
            new(@"\d{18}@discord")
        };

        /// <summary>
        /// Инструмент для проверки соответствия айдишника дискордовскому или стимовскому паттерну.
        /// </summary>
        /// <param name="userId">Стим или дискорд айди.</param>
        /// <returns>true если переданный айди удовлетворяет одному из условий</returns>
        public static bool IsIdValid(string userId)
        {
            return FormatRegex[0].IsMatch(userId) || FormatRegex[1].IsMatch(userId);
        }
    }
}