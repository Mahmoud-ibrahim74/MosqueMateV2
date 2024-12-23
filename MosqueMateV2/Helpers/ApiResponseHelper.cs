using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Resources;

namespace MosqueMateV2.Helpers
{
    public class ApiResponseHelper
    {
        public static Dictionary<PrayerEnum, DateTime> PrayerTimings { get; set; } = [];
        public static object CastTimingsFromApi {  get; set; }  
    }

    public class ApiRquestHelper
    {
        public static string HadithChpterLink(string bookSlug)
        {
            var url = $"https://hadithapi.com/api/{bookSlug}/" +
                        $"chapters?apiKey={SD.Localization.ApiKey}";
            return url;
        }
        public static string HadithCollectionLink(string bookSlug, 
            int chapterNumber,
            HadithStatus status = HadithStatus.Sahih,
            int paginate = 25)
        {
            var url = $"https://hadithapi.com/api/hadiths/?" +
                $"apiKey={SD.Localization.ApiKey}" +
                $"&book={bookSlug}" +
                $"&chapter={chapterNumber}" +
                $"&status={status}" +
                $"&paginate={paginate}";
            return url;
        }
    }
}
