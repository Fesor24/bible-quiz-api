namespace BibleQuiz.API.Dtos.Questions;

public class CreateQuestionDto 
{ 
    public string Question { get; set; } 
    public string Answer { get; set; } 
    public string Book { get; set; }
    public int VerseFrom { get; set; }
    public int VerseTo { get; set;}
    public int Chapter { get; set; }
 }
