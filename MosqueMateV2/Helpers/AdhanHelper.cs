using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
namespace MosqueMateV2.Helpers
{
    public class AdhanHelper
    {
        public static bool IsAlertForNextAdhan { get; private set; }

        public static bool IsAdhanNow { get; private set; }
        public static PrayerEnum? GetCurrentAdhan(Timings _timings)
        {
            TimeSpan tolerance = TimeSpan.FromMinutes(1); // Allow ±1 minute

            if (IsTimeEqual(DateTime.Now, _timings.Fajr, tolerance))
                return PrayerEnum.Fajr;
            if (IsTimeEqual(DateTime.Now, _timings.Sunrise, tolerance))
                return PrayerEnum.Sunrise;
            if (IsTimeEqual(DateTime.Now, _timings.Dhuhr, tolerance))
                return PrayerEnum.Dhuhr;
            if (IsTimeEqual(DateTime.Now, _timings.Asr, tolerance))
                return PrayerEnum.Asr;
            if (IsTimeEqual(DateTime.Now, _timings.Maghrib, tolerance))
                return PrayerEnum.Maghrib;
            if (IsTimeEqual(DateTime.Now, _timings.Isha, tolerance))
                return PrayerEnum.Isha;

            return null; // No Adhan at this time
        }
        public static PrayerEnum? GetNextAdhan(Timings _timings)
        {
            var prayerTimes = new Dictionary<DateTime, PrayerEnum>
        {
            { _timings.Fajr, PrayerEnum.Fajr },
            { _timings.Sunrise, PrayerEnum.Sunrise },
            { _timings.Dhuhr, PrayerEnum.Dhuhr },
            { _timings.Asr, PrayerEnum.Asr },
            { _timings.Maghrib, PrayerEnum.Maghrib },
            { _timings.Isha, PrayerEnum.Isha }
        };

            // Find the next prayer time
            var nextPrayer = prayerTimes
                .Where(pt => pt.Key > DateTime.Now) // Only future prayer times
                .OrderBy(pt => pt.Key)            // Sort by the nearest time
                .FirstOrDefault();

            return nextPrayer.Key > DateTime.MinValue ? nextPrayer.Value : (PrayerEnum?)null;
        }
        public static TimeSpan? GetTimeLeftForNextAdhan(Timings _timings)
        {
            var prayerTimes = new Dictionary<DateTime, PrayerEnum>
            {
                { _timings.Fajr, PrayerEnum.Fajr },
                { _timings.Sunrise, PrayerEnum.Sunrise },
                { _timings.Dhuhr, PrayerEnum.Dhuhr },
                { _timings.Asr, PrayerEnum.Asr },
                { _timings.Maghrib, PrayerEnum.Maghrib },
                { _timings.Isha, PrayerEnum.Isha }
            };

            // Find the next prayer time
            var nextPrayer = prayerTimes
                .Where(pt => pt.Key > DateTime.Now) // Only future prayer times
                .OrderBy(pt => pt.Key)            // Sort by the nearest time
                .FirstOrDefault();

            if (nextPrayer.Key > DateTime.MinValue)
            {
                var res = nextPrayer.Key - DateTime.Now;
                var roundTime = Math.Round(res.TotalMinutes,2);
                IsAlertForNextAdhan = roundTime == 10.8; // Make it Optional Settings
                IsAdhanNow =        ((nextPrayer.Key - DateTime.Now).Hours) == 0
                                    && ((nextPrayer.Key - DateTime.Now).Minutes) == 0
                                    && ((nextPrayer.Key - DateTime.Now).Seconds) == 0;
                return nextPrayer.Key - DateTime.Now;
            }


            return null; // No more Adhan times today
        }
        private static bool IsTimeEqual(DateTime currentTime, DateTime prayerTime, TimeSpan tolerance)
        {
            return Math.Abs((currentTime - prayerTime).TotalMinutes) <= tolerance.TotalMinutes;
        }
    }
}
