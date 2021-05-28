using System;
using System.Globalization;

namespace TechAnswers.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this string value, string dateformat)
        {
            DateTime dateTimeValue = DateTime.Now;
            var timeStamp = value;
            var subdateTime = timeStamp.Substring(0, timeStamp.Length - 2);
            DateTime.TryParseExact(subdateTime,
                dateformat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal,
                out dateTimeValue);
            return dateTimeValue;
        }

        public static DateTime ToStartOfDay(this DateTime value)
        {
            return value.Date.AddHours(0).AddMinutes(0).AddSeconds(0).AddMilliseconds(0);
        }

        public static DateTime ToEndOfDay(this DateTime value)
        {
            return value.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }
    }
}
