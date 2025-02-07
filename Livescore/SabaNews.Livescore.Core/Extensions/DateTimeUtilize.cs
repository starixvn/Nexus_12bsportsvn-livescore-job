namespace SabaNews.Livescore.Core.Extensions
{
    using System;
    using System.Globalization;

    public static class DateTimeUtilize
    {
        private const int ClientTimezone = 7;

        public static string ChangeTimeZoneToClient(DateTimeOffset time)
            => time.AddHours(ClientTimezone).ToString("dd MMM yyyy HH:mm:ssss");

        public static string FormatDateTime(DateTimeOffset time, string Format)
            => time.AddHours(ClientTimezone).ToString(Format, new CultureInfo("vi"));

        public static DateTime ConvertToGMT(this DateTime time)
            => time.AddHours(-ClientTimezone);

        public static DateTimeOffset ConvertToClient(this DateTimeOffset dateTime)
            => dateTime.AddHours(ClientTimezone);

        public static DateTime ToEndOfDay(this DateTime date)
            => new DateTimeOffset(date.Year, date.Month, date.Day, 23, 59, 59,
                TimeSpan.FromHours(ClientTimezone)).DateTime;

        public static DateTimeOffset UtcNow() => DateTimeOffset.UtcNow;

        public static DateTimeOffset Now() => DateTimeOffset.Now;

        public static DateTime ToStartOfDay(this DateTime date)
            => new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0,
                TimeSpan.FromHours(ClientTimezone)).DateTime;

        public static DateTime? UnixTimeStampToDateTime(double unixTimeStamp)
        => unixTimeStamp > 0 ? DateTimeOffset.UnixEpoch.AddSeconds(unixTimeStamp).DateTime : default;
    }
}