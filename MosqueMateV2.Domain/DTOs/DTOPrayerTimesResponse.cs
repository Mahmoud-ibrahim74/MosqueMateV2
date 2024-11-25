using Newtonsoft.Json;

namespace MosqueMateV2.Domain.DTOs
{
    public class DTOPrayerTimesResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public PrayerTimesData Data { get; set; }
    }

    public class PrayerTimesData
    {
        [JsonProperty("timings")]
        public Timings Timings { get; set; }

        [JsonProperty("date")]
        public DateInfo Date { get; set; }

        [JsonProperty("meta")]
        public MetaInfo Meta { get; set; }
    }

    public class Timings
    {
        [JsonProperty(nameof(Fajr))]
        public DateTime Fajr { get; set; }

        [JsonProperty(nameof(Sunrise))]
        public DateTime Sunrise { get; set; }

        [JsonProperty(nameof(Dhuhr))]
        public DateTime Dhuhr { get; set; }

        [JsonProperty(nameof(Asr))]
        public DateTime Asr { get; set; }

        [JsonProperty(nameof(Sunset))]
        public DateTime Sunset { get; set; }

        [JsonProperty(nameof(Maghrib))]
        public DateTime Maghrib { get; set; }

        [JsonProperty(nameof(Isha))]
        public DateTime Isha { get; set; }

        [JsonProperty(nameof(Imsak))]
        public DateTime Imsak { get; set; }

        [JsonProperty(nameof(Midnight))]
        public DateTime Midnight { get; set; }

        [JsonProperty(nameof(Firstthird))]
        public DateTime Firstthird { get; set; }

        [JsonProperty(nameof(Lastthird))]
        public DateTime Lastthird { get; set; }
    }

    public class DateInfo
    {
        [JsonProperty("readable")]
        public string Readable { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("hijri")]
        public HijriInfo Hijri { get; set; }

        [JsonProperty("gregorian")]
        public GregorianInfo Gregorian { get; set; }
    }

    public class HijriInfo
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("weekday")]
        public WeekdayInfo Weekday { get; set; }

        [JsonProperty("month")]
        public MonthInfo Month { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("designation")]
        public DesignationInfo Designation { get; set; }

        [JsonProperty("holidays")]
        public string[] Holidays { get; set; }
    }

    public class GregorianInfo
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("weekday")]
        public WeekdayInfo Weekday { get; set; }

        [JsonProperty("month")]
        public MonthInfo Month { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("designation")]
        public DesignationInfo Designation { get; set; }
    }

    public class WeekdayInfo
    {
        [JsonProperty("en")]
        public string English { get; set; }

        [JsonProperty("ar")]
        public string Arabic { get; set; }
    }

    public class MonthInfo
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("en")]
        public string English { get; set; }

        [JsonProperty("ar")]
        public string Arabic { get; set; }
    }

    public class DesignationInfo
    {
        [JsonProperty("abbreviated")]
        public string Abbreviated { get; set; }

        [JsonProperty("expanded")]
        public string Expanded { get; set; }
    }

    public class MetaInfo
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("method")]
        public MethodInfo Method { get; set; }

        [JsonProperty("latitudeAdjustmentMethod")]
        public string LatitudeAdjustmentMethod { get; set; }

        [JsonProperty("midnightMode")]
        public string MidnightMode { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }

        [JsonProperty("offset")]
        public OffsetInfo Offset { get; set; }
    }

    public class MethodInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("params")]
        public MethodParams Params { get; set; }

        [JsonProperty("location")]
        public LocationInfo Location { get; set; }
    }

    public class MethodParams
    {
        [JsonProperty("Fajr")]
        public double Fajr { get; set; }

        [JsonProperty("Isha")]
        public string Isha { get; set; }
    }

    public class LocationInfo
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class OffsetInfo
    {
        [JsonProperty("Imsak")]
        public int Imsak { get; set; }

        [JsonProperty("Fajr")]
        public int Fajr { get; set; }

        [JsonProperty("Sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("Dhuhr")]
        public int Dhuhr { get; set; }

        [JsonProperty("Asr")]
        public int Asr { get; set; }

        [JsonProperty("Maghrib")]
        public int Maghrib { get; set; }

        [JsonProperty("Sunset")]
        public int Sunset { get; set; }

        [JsonProperty("Isha")]
        public int Isha { get; set; }

        [JsonProperty("Midnight")]
        public int Midnight { get; set; }
    }
}
