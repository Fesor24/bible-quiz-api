namespace BibleQuiz.Core
{
    public class BibleApiRequest
    {
        public string Uri { get; set; }

        public HttpMethod Method { get; set; }

        public Dictionary<string, string> Headers { get; set; }
    }

    public class BibleApiRequest<TBody>: BibleApiRequest
    {
        public TBody Content { get; set;}
    }
}
