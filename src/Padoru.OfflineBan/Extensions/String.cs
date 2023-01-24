namespace Padoru.OfflineBan.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Преобразуем строчное время формата 1D // 50Y // 6M в секунды
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="seconds">Время в секундах</param>
        /// <returns>Успешно ли преобразование</returns>
        public static bool RelativeTimeToSeconds(this string time, out long seconds)
        {
            seconds = 0;

            if (time.Length < 2 || !long.TryParse(time.Substring(0, time.Length - 1), out var parsedTime))
            {
                return false;
            }

            switch (time[time.Length - 1])
            {
                case 's':
                    seconds = parsedTime;
                    break;
                case 'm':
                    seconds = parsedTime * 60;
                    break;
                case 'h':
                    seconds = parsedTime * 3600;
                    break;
                case 'D':
                    seconds = parsedTime * 86400;
                    break;
                case 'M':
                    seconds = parsedTime * 2592000;
                    break;
                case 'Y':
                    seconds = parsedTime * 31536000;
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}