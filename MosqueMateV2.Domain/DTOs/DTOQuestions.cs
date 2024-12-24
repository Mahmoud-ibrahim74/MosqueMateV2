namespace MosqueMateV2.Domain.DTOs
{
    public class DTOQuestions
    {
        public int id { get; set; }
        public string q { get; set; }
        public int level { get; set; }
        public string link { get; set; }
        public string section { get; set; }
        public List<Answer> answers { get; set; }
    }
    public class Answer
    {
        public string answer { get; set; }
        public int t { get; set; }
    }

}
