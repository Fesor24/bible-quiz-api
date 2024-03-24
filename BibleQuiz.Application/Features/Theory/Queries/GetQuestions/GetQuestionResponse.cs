namespace BibleQuiz.Application.Features.Theory.Queries.GetQuestions;
public class GetQuestionResponse
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Verse { get; set; }
    public string Source { get; set; }
    public string Passage { get; set; }
}
