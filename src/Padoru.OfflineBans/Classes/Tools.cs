namespace Padoru.OfflineBans.Classes
{
    using PluginAPI.Helpers;
    using System.Text.RegularExpressions;

    public static class Tools
    {
        public static string dirname { get; } = "\\Wanted";

        public static string filepath { get; } = Paths.PluginAPI + dirname;

        public static Regex[] Regex { get; } = { new Regex(@"/^\d{17}@steam\s\d*.\s.*/"),
                                          new Regex(@"/^\d{18}@discord\s\d*.\s.*/")};

        public static Regex[] RegexDel { get; } = { new Regex(@"/^\d{18}@discord\s.*/"),
                                             new Regex(@"/^\d{17}@steam\s.*/")};

        public static (long, string) GetBanTime(string banstring)
        {
            long bantime;
            string postfix;

            bantime = GetFullTimeSeconds(banstring);
            if (bantime == -1L)
            {
                return (bantime, "");
            }
            postfix = PostfixCreator(bantime);

            return (bantime, postfix);
        }

        private static string PostfixCreator(long bantime)
        {
            int k = 0;
            string result = bantime + "сек.";

            //if (bantime >= 311004000)
            //{
            //    while (bantime > 2592000)
            //    {
            //        bantime /= 311004000;
            //        k++;
            //    }

            //    result += k + " г. ";
            //    k = 0;
            //}

            //if (bantime >= 2592000)
            //{
            //    while (bantime > 86400)
            //    {
            //        bantime /= 2592000;
            //        k++;
            //    }

            //    result += k + " мес. ";
            //    k = 0;
            //}

            //if (bantime >= 86400)
            //{
            //    while (bantime > 3600)
            //    {
            //        bantime /= 86400;
            //        k++;
            //    }

            //    result += k + " дн. ";
            //    k = 0;
            //}

            //if (bantime >= 3600)
            //{
            //    while (bantime > 60)
            //    {
            //        bantime /= 3600;
            //        k++;
            //    }

            //    result += k + " ч. ";
            //    k = 0;
            //}

            //if (bantime >= 60)
            //{
            //    while (bantime > 1)
            //    {
            //        bantime /= 60;
            //        k++;
            //    }

            //    result += k + " мин. ";
            //}

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
                    if (k == 0)
                    {
                        return -1L;
                    }
                    substring = banstring.Substring(previousLetter, currentChar - previousLetter);
                    bantime += k * long.Parse(substring);
                    previousLetter = banstring.IndexOf(c) + 1;
                }
                currentChar++;
                if (currentChar == 21)
                {
                    return -1L;
                }
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
