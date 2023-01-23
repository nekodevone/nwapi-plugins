using System.IO;
using System.Linq;
using static BanHandler;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.OfflineBans.Classes
{
    public class WantedUser
    {
        public string ID { get; set; }

        public long BanTime { get; set; }

        public string Reason { get; set; }

        private static char[] TimeTags { get; } = new char[] { 'm', 'h', 'D', 'M', 'Y' };

        public WantedUser(string steamID, long banTime, string reason)
        {
            ID = steamID;
            BanTime = banTime;
            Reason = reason;
        }

        public WantedUser() { }

        /// <summary>
        /// Банит игрока на срок и с причиной из json-файла, после чего удаляет этот файл.
        /// </summary>
        /// <param name="player">Игрок.</param>
        public static void Ban(PlayerAPI player)
        {
            string path = Tools.FolderPath + $"\\{player.UserId}.json";
            string json = File.ReadAllText(path);

            WantedUser user = Utf8Json.JsonSerializer.Deserialize<WantedUser>(json);
            player.Ban(user.Reason, user.BanTime);
            File.Delete(path);
        }

        /// <summary>
        /// Проверяет наличие json-файла с айди игрока в папке розыска.
        /// </summary>
        /// <param name="userId">Айди игрока.</param>
        /// <returns>true если файл существует.</returns>
        public static bool Has(string userId)
        {
            if (File.Exists(Path.Combine(Tools.FolderPath, $"{userId}.json")))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// В папке розыска создает json-файл розыска с именем-айди игрока и сериализует туда класс WantedUser.
        /// </summary>
        /// <param name="userId">Айди игрока.</param>
        /// <param name="time">Срок бана.</param>
        /// <param name="reason">Причина бана.</param>
        public static void Add(string userId, long time, string reason)
        {
            WantedUser user = new WantedUser(userId, time, reason);
            string json = Utf8Json.JsonSerializer.ToJsonString(user);
            File.WriteAllText(Tools.FolderPath + $"\\{userId}.json", json);
        }

        /// <summary>
        /// Проверяет строку с временем бана на соответствие формату.
        /// </summary>
        /// <param name="bantime">Время бана.</param>
        /// <returns>true если время бана записано в читаемом формате.</returns>
        public static bool TimeFormatCheck(string bantime)
        {
            int previousLetter = -1, currentChar = 0;
            if (!char.IsDigit(bantime.First()))
            {
                return false;
            }

            if (!char.IsLetter(bantime.Last()))
            {
                return false;
            }

            foreach (char c in bantime)
            {
                if (!char.IsDigit(c))
                {
                    if (currentChar - previousLetter == 1)
                    {
                        return false;
                    }

                    if (!TimeTags.Contains(c))
                    {
                        return false;
                    }

                    previousLetter = currentChar;
                }

                currentChar++;
            }

            return true;
        }

        /// <summary>
        /// Переводит строку со сроком бана в читаемый для программы формат и на русский язык.
        /// </summary>
        /// <param name="banstring">Строка со сроком бана</param>
        /// <returns>Длительность бана в секундах и строку с временем бана на русском.</returns>
        public static (long, string) GetBanTime(string banstring)
        {
            long bantime;
            string postfix;

            bantime = GetFullTimeSeconds(banstring);
            postfix = PostfixCreator(bantime);

            return (bantime, postfix);
        }

        /// <summary>
        /// Создаёт строку со сроком бана на русском.
        /// </summary>
        /// <param name="bantime">Срок бана в секундах.</param>
        /// <returns>Русскую строку :loh:</returns>
        private static string PostfixCreator(long bantime)
        {
            string result = bantime + "сек.";

            // место для реализации перевода времени бана в более комфортный вид

            return result;
        }

        /// <summary>
        /// Переводит строку со сроком бана в секунды.
        /// </summary>
        /// <param name="banstring">Строка со сроком бана.</param>
        /// <returns>Срок бана в секундах.</returns>
        private static long GetFullTimeSeconds(string banstring)
        {
            long bantime = 0, k;
            string substring;
            int previousLetter = 0, currentChar = 0;

            foreach (char c in banstring)
            {
                if (!char.IsDigit(c))
                {
                    k = GetCoeff(c);
                    substring = banstring.Substring(previousLetter, currentChar - previousLetter);
                    bantime += k * long.Parse(substring);
                    previousLetter = banstring.IndexOf(c) + 1;
                }
                currentChar++;
            }

            return bantime;
        }

        /// <summary>
        /// Принимает единицу измерения времени и преващает её в секунды.
        /// </summary>
        /// <param name="c">Символ с единицей измерения.</param>
        /// <returns>Количество секунд в единице времени.</returns>
        private static long GetCoeff(char c)
        {
            switch (c)
            {
                case 'm':
                    return 60L;

                case 'h':
                    return 3600L;

                case 'D':
                    return 86400L;

                case 'M':
                    return 2592000L;

                case 'Y':
                    return 31557600L;

                default:
                    return 0;
            }
        }
    }
}
