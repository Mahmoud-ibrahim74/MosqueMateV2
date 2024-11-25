using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
namespace MosqueMateV2.Helpers
{
    public class AdhanHelper
    {
        public static PrayerEnum? GetCurrentAdhan(Timings _timings)
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan tolerance = TimeSpan.FromMinutes(1); // Allow ±1 minute

            if (IsTimeEqual(currentTime, _timings.Fajr, tolerance))
                return PrayerEnum.Fajr;
            if (IsTimeEqual(currentTime, _timings.Sunrise, tolerance))
                return PrayerEnum.Sunrise;
            if (IsTimeEqual(currentTime, _timings.Dhuhr, tolerance))
                return PrayerEnum.Dhuhr;
            if (IsTimeEqual(currentTime, _timings.Asr, tolerance))
                return PrayerEnum.Asr;
            if (IsTimeEqual(currentTime, _timings.Maghrib, tolerance))
                return PrayerEnum.Maghrib;
            if (IsTimeEqual(currentTime, _timings.Isha, tolerance))
                return PrayerEnum.Isha;

            return null; // No Adhan at this time
        }
        public static PrayerEnum? GetNextAdhan(Timings _timings)
        {
            DateTime currentTime = DateTime.Now;

            // Create a dictionary of prayer times and their enums
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
                .Where(pt => pt.Key > currentTime) // Only future prayer times
                .OrderBy(pt => pt.Key)            // Sort by the nearest time
                .FirstOrDefault();

            return nextPrayer.Key > DateTime.MinValue ? nextPrayer.Value : (PrayerEnum?)null;
        }
        public static TimeSpan? GetTimeLeftForNextAdhan(Timings _timings)
        {
            DateTime currentTime = DateTime.Now;

            // Create a dictionary of prayer times and their enums
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
                .Where(pt => pt.Key > currentTime) // Only future prayer times
                .OrderBy(pt => pt.Key)            // Sort by the nearest time
                .FirstOrDefault();

            if (nextPrayer.Key > DateTime.MinValue)
            {
                return nextPrayer.Key - currentTime; // Calculate the remaining time
            }

            return null; // No more Adhan times today
        }
        private static bool IsTimeEqual(DateTime currentTime, DateTime prayerTime, TimeSpan tolerance)
        {
            return Math.Abs((currentTime - prayerTime).TotalMinutes) <= tolerance.TotalMinutes;
        }
    }
}
