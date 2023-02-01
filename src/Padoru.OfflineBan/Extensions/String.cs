using System;

namespace Padoru.OfflineBan.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Преобразуем строчное время формата 6M:1D:30m
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="divider">Разделитель</param>
        /// <param name="seconds">Время в секундах</param>
        /// <returns>Успешно ли преобразование</returns>
        public static bool RelativeTimeToSeconds(this string time, string divider, out long seconds)
        {
            var splitTime = time.Split(new[] { divider }, StringSplitOptions.RemoveEmptyEntries);

            seconds = 0;

            foreach (var s in splitTime)
            {
                if (s.Length < 2 || !long.TryParse(s.Substring(0, s.Length - 1), out var parsedTime))
                {
                    return false;
                }

                switch (s[s.Length - 1])
                {
                    case 's':
                        seconds += parsedTime;
                        break;
                    case 'm':
                        seconds += parsedTime * 60;
                        break;
                    case 'h':
                        seconds += parsedTime * 3600;
                        break;
                    case 'D':
                        seconds += parsedTime * 86400;
                        break;
                    case 'M':
                        seconds += parsedTime * 2592000;
                        break;
                    case 'Y':
                        seconds += parsedTime * 31536000;
                        break;
                    default:
                        return false;
                }
            }
            
            return true;
        }
    }
}