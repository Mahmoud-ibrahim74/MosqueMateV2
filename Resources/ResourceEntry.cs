namespace MosqueMateV2.Resources
{
    public record ResourceEntry
    {
        public string Name { get; init; }
        public dynamic? Value { get; init; }
        public string? Comment { get; init; }
    }
    public enum ResourceTypeEnum
    {
        FileResources = 1,
        FontResources = 2,
        MediaResources = 3
    }
}
