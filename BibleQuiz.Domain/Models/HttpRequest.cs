namespace BibleQuiz.Domain.Models;
public class HttpRequest
{
    public string Uri { get; set; }

    public HttpMethod Method { get; set; }

    public Dictionary<string, string> Headers { get; set; } = new();
}

public class HttpRequest<TBody> : HttpRequest
{
    public TBody Content { get; set; }
}
