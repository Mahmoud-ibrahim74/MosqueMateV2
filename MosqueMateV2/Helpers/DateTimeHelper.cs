using Resources;

namespace MosqueMateV2.Helpers
{
    public class DateTimeHelper
    {
        public static string PrintGreeting()
        {
            var currentHour = DateTime.Now.Hour;

            if (currentHour < 12)
                return App.LocalizationService[AppLocalization.GoodMorning];
            else
                return App.LocalizationService[AppLocalization.GoodAfternoon];

        }
    }
}
