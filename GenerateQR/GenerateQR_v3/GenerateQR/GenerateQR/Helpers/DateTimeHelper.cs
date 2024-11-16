using System.Globalization;
using System.Runtime.InteropServices;

namespace GenerateQR.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetKSADateTime()
        {
            TimeZoneInfo KSATimeZone = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                KSATimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                KSATimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Riyadh");
            }
            DateTimeOffset original = DateTime.UtcNow;
            DateTimeOffset cetTime = TimeZoneInfo.ConvertTime(original, KSATimeZone);
            return cetTime.DateTime;
        }

        public static string GetKSAStrDate()
        {
            DateTimeOffset dateTime = GetKSADateTime();
            var strDateTime = dateTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return strDateTime;
        }
        public static DateTime NphiesDateTime()
        {
            DateTime dateTime = GetKSADateTime();
            var strDateTime = dateTime.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return DateTime.Parse(strDateTime, CultureInfo.InvariantCulture);
        }
    }
}
