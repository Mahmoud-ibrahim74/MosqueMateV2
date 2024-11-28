namespace MosqueMateV2.Domain.DTOs
{
    public record DTOCity
    {
        public string city { get; init; }
        public string country { get; init; }
        public List<PopulationCount> populationCounts { get; init; }
    }
    public class PopulationCount
    {
        public string year { get; init; }
        public string value { get; init; }
        public string sex { get; init; }
        public string reliabilty { get; init; }
    }

}
