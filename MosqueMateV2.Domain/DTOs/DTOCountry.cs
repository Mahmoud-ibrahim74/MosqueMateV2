namespace MosqueMateV2.Domain.DTOs
{
    public record DTOCountry
    {
        public string english_name { get; init; }
        public string arabic_name { get; init; }
        public string alpha2_code { get; init; }
        public string alpha3_code { get; init; }
        public string phone_code { get; init; }
    }
}
