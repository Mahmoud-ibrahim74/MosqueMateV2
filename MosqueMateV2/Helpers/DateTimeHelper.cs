using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Resources;

namespace MosqueMateV2.Helpers
{
    public class DateTimeHelper
    {
        public static string PrintGreeting()
        {
            var currentHour = DateTime.Now.Hour;

            if (currentHour < 12)
                return App.LocalizationService[SD.Localization.GoodMorning];
            else
                return App.LocalizationService[SD.Localization.GoodEvening];
        }
        public static string GetNightOfDay(DateTime magribTime)
        {
            //if (magribTime is not null)
            //    return "pack://application:,,,/Assets/night.png";

            var currentHour = DateTime.Now;
            if (currentHour > magribTime)
                return "pack://application:,,,/Assets/night.png";
            else
                return "pack://application:,,,/Assets/sunny.png";
        }
    }
}
