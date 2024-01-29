namespace BibleQuiz.Domain.Models;
public class BibleCredentials
{
    public const string CONFIGURATION = "BibleApi";
    public string ApiKey { get; set; }
    public string Url { get; set; }
    public string KjvSpec { get; set; }
}
