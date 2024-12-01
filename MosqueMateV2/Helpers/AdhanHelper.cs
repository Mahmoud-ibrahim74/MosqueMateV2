using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Properties;
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


            if (nextPrayer.Key > DateTime.MinValue)
            {
                // Return the next prayer for today
                return nextPrayer.Value;
            }

            // If no prayer remains for today, return Fajr of the next day
            return PrayerEnum.Fajr;
        }
        public static TimeSpan? GetTimeLeftForNextAdhan(Timings timings)
        {
            var now = DateTime.Now;

            // Create a dictionary of prayer times for today
            var prayerTimes = new Dictionary<DateTime, PrayerEnum>
                    {
                        { timings.Fajr, PrayerEnum.Fajr },
                        { timings.Sunrise, PrayerEnum.Sunrise },
                        { timings.Dhuhr, PrayerEnum.Dhuhr },
                        { timings.Asr, PrayerEnum.Asr },
                        { timings.Maghrib, PrayerEnum.Maghrib },
                        { timings.Isha, PrayerEnum.Isha }
                    };

            // Find the next prayer for today
            var nextPrayer = prayerTimes
                .Where(pt => pt.Key > now) // Only future prayer times for today
                .OrderBy(pt => pt.Key)    // Sort by the nearest time
                .FirstOrDefault();

            if (nextPrayer.Key > DateTime.MinValue)
            {
                // Calculate time left for the next prayer
                var timeLeft = nextPrayer.Key - now;

                // Optional settings
                IsAlertForNextAdhan = Math.Round(timeLeft.TotalMinutes, 2) == AppSettings.Default.TimeRemainder.TotalMinutes;
                IsAdhanNow = timeLeft.Hours == 0 && timeLeft.Minutes == 0 && timeLeft.Seconds == 0;
                return timeLeft;
            }

            // If no prayer remains for today, calculate time left for Fajr of the next day
            var fajrTomorrow = timings.Fajr.AddDays(1);
            var timeLeftForFajr = fajrTomorrow - now;

            // Optional settings for Fajr
            IsAlertForNextAdhan = Math.Round(timeLeftForFajr.TotalMinutes, 2) == AppSettings.Default.TimeRemainder.TotalMinutes;
            IsAdhanNow = timeLeftForFajr.Hours == 0 && timeLeftForFajr.Minutes == 0 && timeLeftForFajr.Seconds == 0;

            return timeLeftForFajr;
        }

        private static bool IsTimeEqual(DateTime currentTime, DateTime prayerTime, TimeSpan tolerance)
        {
            return Math.Abs((currentTime - prayerTime).TotalMinutes) <= tolerance.TotalMinutes;
        }
    }
}
