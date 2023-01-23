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
        /// Проверяет переданный айдишник на соответствие дискордовскому или стимовскому.
        /// </summary>
        /// <param name="userId">Стим или дискорд айди.</param>
        /// <returns>true если переданный айди удовлетворяет одному из условий</returns>
        public static bool IsIdValid(string userId)
        {
            if (Tools.FormatRegex[0].IsMatch(userId) || Tools.FormatRegex[1].IsMatch(userId))
            {
                return true;
            }

            return false;
        }
    }
}
