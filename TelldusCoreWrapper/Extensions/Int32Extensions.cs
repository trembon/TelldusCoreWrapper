using System;
using System.Collections.Generic;
using System.Text;

namespace TelldusCoreWrapper.Extensions
{
    internal static class Int32Extensions
    {
        public static DateTime ToDateTime(this int value)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(value);
            return dateTime;
        }

        public static DateTime ToLocalDateTime(this int value)
        {
            return value.ToDateTime().ToLocalTime();
        }
    }
}
