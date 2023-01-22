using System.IO;
using System.Linq;
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

        public static void Ban(PlayerAPI player)
        {
            string path = Tools.FolderPath + $"\\{player.UserId}.json";
            string json = File.ReadAllText(path);

            WantedUser user = Utf8Json.JsonSerializer.Deserialize<WantedUser>(json);
            player.Ban(user.Reason, user.BanTime);
            File.Delete(path);
        }

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

        public static (long, string) GetBanTime(string banstring)
        {
            long bantime;
            string postfix;

            bantime = GetFullTimeSeconds(banstring);
            postfix = PostfixCreator(bantime);

            return (bantime, postfix);
        }

        private static string PostfixCreator(long bantime)
        {
            string result = bantime + "сек.";

            // место для реализации перевода времени бана в более комфортный вид

            return result;
        }

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
