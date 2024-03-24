namespace BibleQuiz.API.Dtos.Questions;

public class CreatePastQuestionsDto
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}
