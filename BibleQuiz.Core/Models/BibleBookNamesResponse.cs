namespace BibleQuiz.Core
{
    public class BibleBookNamesResponse
    {
        public List<BibleData> Data { get; set; }
    }

    public class BibleData
    {
        public string Id { get; set; }

        public string BibleId { get; set; }

        public string Abbreviation { get; set; }

        public string Name { get; set; }

        public string NameLong { get; set; }
    }
}
