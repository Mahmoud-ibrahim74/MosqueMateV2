using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Domain.DTOs
{
    public class DTOChapter
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<Chapter> chapters { get; set; } = [];
    }
    public class Chapter
    {
        public int id { get; set; }
        public string chapterNumber { get; set; }
        public string chapterEnglish { get; set; }
        public string chapterUrdu { get; set; }
        public string chapterArabic { get; set; }
        public string bookSlug { get; set; }
    }
}
