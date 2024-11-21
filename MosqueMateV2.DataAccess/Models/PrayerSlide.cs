namespace MosqueMateV2.DataAccess.Models
{
    public class PrayerSlide
    {
        public int id { get; set; }
        public string ImagePath { get; set; }
        public string? CurrentPrayerName { get; set; }
        public string? CurrentPrayerTime { get; set; }
    }
}
