using MosqueMateV2.Resources;
using System.Globalization;

namespace MosqueMateV2.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToLocalizedDate(this DateTime dateTime, string lang = AppLocalization.English)
        {
            var cultureInfo = new CultureInfo(lang);
            return dateTime.ToString("D", cultureInfo);
        }
    }
}
