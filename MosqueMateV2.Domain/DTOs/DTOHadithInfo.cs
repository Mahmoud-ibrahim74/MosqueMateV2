namespace MosqueMateV2.Domain.DTOs
{
    public class DTOHadithInfo
    {
        public int status { get; set; }
        public string message { get; set; }
        public Hadiths hadiths { get; set; }
    }
    public class BookInfo
    {
        public int id { get; set; }
        public string bookName { get; set; }
        public string writerName { get; set; }
        public object aboutWriter { get; set; }
        public string writerDeath { get; set; }
        public string bookSlug { get; set; }
    }

    public class ChapterInfo
    {
        public int id { get; set; }
        public string chapterNumber { get; set; }
        public string chapterEnglish { get; set; }
        public string chapterUrdu { get; set; }
        public string chapterArabic { get; set; }
        public string bookSlug { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public string hadithNumber { get; set; }
        public string englishNarrator { get; set; }
        public string hadithEnglish { get; set; }
        public string hadithUrdu { get; set; }
        public string urduNarrator { get; set; }
        public string hadithArabic { get; set; }
        public string headingArabic { get; set; }
        public string headingUrdu { get; set; }
        public string headingEnglish { get; set; }
        public string chapterId { get; set; }
        public string bookSlug { get; set; }
        public string volume { get; set; }
        public string status { get; set; }
        public BookInfo book { get; set; }
        public ChapterInfo chapter { get; set; }
    }

    public class Hadiths
    {
        public int current_page { get; set; }
        public List<Data> data { get; set; }
        public string first_page_url { get; set; }
        public int from { get; set; }
        public int last_page { get; set; }
        public string last_page_url { get; set; }
        public List<Link> links { get; set; }
        public string next_page_url { get; set; }
        public string path { get; set; }
        public string per_page { get; set; }
        public object prev_page_url { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }

    public class Link
    {
        public string url { get; set; }
        public string label { get; set; }
        public bool active { get; set; }
    }




}
