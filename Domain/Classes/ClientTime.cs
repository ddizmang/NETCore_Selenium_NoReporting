using NETCore_Selenium.Domain.Enums;
using System;

namespace NETCore_Selenium.Domain.Classes
{
    public static class ClientTime
    {
        public static DateTime ConvertToMountainTime(DateTime utc)
        {
            if (utc == null)
                throw new Exception("Tried to convert a null date time to local date time.");

            var fromUtcDateTime = utc;
            var toUtcDateTime = DateTime.SpecifyKind(fromUtcDateTime, DateTimeKind.Utc);
            var timeZone = "Mountain Standard Time";
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTime(toUtcDateTime, timeZoneInfo);
        }
    }
}