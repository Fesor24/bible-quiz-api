namespace BibleQuiz.Core
{
    public class BibleApiVerseSearchResponse
    {
        public Data Data { get; set; }

        public Meta Meta { get; set; }
    }

    public class Data
    {
        public List<Passages> Passages { get; set; }
    }

    public class Passages
    {
        public string Id { get; set; }

        public string OrgId { get; set; }

        public string BibleId { get; set; }

        public string BookId { get; set; }

        public string[] ChapterIds { get; set; }

        public string Reference { get; set; }

        public string Content { get; set; }

        public int VerseCount { get; set; }

        public string Copyright { get; set; }
    }

    public class Meta
    {
        public string Fums { get; set; }

        public string FumsId { get; set; }

        public string FumsJsInclude { get; set; }

        public string FumsJs { get; set; }

        public string FumsNoScript { get; set; }
    }
}
