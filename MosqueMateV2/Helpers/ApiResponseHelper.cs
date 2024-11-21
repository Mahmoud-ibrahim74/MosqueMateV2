using MosqueMateV2.Domain.Enums;

namespace MosqueMateV2.Helpers
{
    public class ApiResponseHelper
    {
        public static Dictionary<PrayerEnum, DateTime> PrayerTimings { get; set; } = [];
        public static object CastTimingsFromApi {  get; set; }  
    }
}
