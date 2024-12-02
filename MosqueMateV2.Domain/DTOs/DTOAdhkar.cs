namespace MosqueMateV2.Domain.DTOs
{
    public record DTOAdhkar
    {
        public int id { get; init; }
        public string category { get; init; }
        public string audio { get; init; }
        public string filename { get; init; }
        public List<ZekrContent> zekrContent { get; init; }
    }
    public record ZekrContent
    {
        public int id { get; init; }
        public string text { get; init; }
        public int count { get; init; }
        public string audio { get; init; }
        public string filename { get; init; }
    }
}
