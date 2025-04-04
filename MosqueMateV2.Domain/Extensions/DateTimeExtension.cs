﻿using MosqueMateV2.Resources;
using System.Globalization;

namespace MosqueMateV2.Domain.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToLocalizedDate(this DateTime dateTime, string lang = SD.Localization.English)
        {
            var cultureInfo = new CultureInfo(lang);
            return dateTime.ToString("D", cultureInfo);
        }
    }
}
