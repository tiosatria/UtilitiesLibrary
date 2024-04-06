using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary
{
    public static class Date
    {
        public enum RegionTZ
        {
            id,
            ph,
            vi,
            br,
            kr
        }

        public static DateTime GetTimeNowByRegion(RegionTZ region)
        {
            /*TimeZoneInfo zone = region == RegionTZ.id || region == RegionTZ.vi ? TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time") : TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");*/
            var zone = region == RegionTZ.id || region == RegionTZ.vi ? TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time") :
                region == RegionTZ.br ? TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time") :
                region == RegionTZ.kr ? TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time") :
                TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zone);
        }

        public static DateTime ConvertTimeToUtc(DateTime dt, RegionTZ region)
        {
            var zone = region == RegionTZ.id || region == RegionTZ.vi ? TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time") :
                region == RegionTZ.br ? TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time") :
                region == RegionTZ.kr ? TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time") :
                TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            return TimeZoneInfo.ConvertTimeToUtc(dt, zone);
        }

        public static long GetUnixTimeMS(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixMS(long unixTimeMS)
        {
            var offset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMS);
            return offset.DateTime;
        }

        public static (DateTime now, DateTime startDay, DateTime endDay) GetTimeBetween(string region)
        {
            var reg = RegionTZ.id;
            var parsed = Enum.TryParse(region, true, out RegionTZ _reg);
            if (parsed) reg = _reg;
            var now = GetTimeNowByRegion(reg);
            var start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            var end = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            return (now,start,end);
        }

    }
}
