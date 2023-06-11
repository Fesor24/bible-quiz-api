namespace BibleQuiz.Core
{
    public class BibleBooksApiModel
    {
        public int Id { get; set; }

        public string BibleId { get; set; }

        public string ShortName { get; set; }

        public string ShortNameUpper { get; set; }

        public string LongName { get; set; }
    }
}
