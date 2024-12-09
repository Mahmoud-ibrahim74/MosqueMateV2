namespace MosqueMateV2.Domain.DTOs
{
    public class DTOBookResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<Book> books { get; set; }
    }
    public class Book
    {
        public int id { get; set; }
        public string bookName { get; set; }
        public string writerName { get; set; }
        public string aboutWriter { get; set; }
        public string writerDeath { get; set; }
        public string bookSlug { get; set; }
        public string hadiths_count { get; set; }
        public string chapters_count { get; set; }
    }

}
