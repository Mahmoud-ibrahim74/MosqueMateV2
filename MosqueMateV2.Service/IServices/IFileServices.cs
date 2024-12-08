namespace MosqueMateV2.Service.IServices
{
    public interface IFileServices
    {
        public string AppTempPath { get; set; }
        public string CombinePathWithTemp(params string[] pathes);

    }
}
