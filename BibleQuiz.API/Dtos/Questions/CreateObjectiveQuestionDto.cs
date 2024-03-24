namespace BibleQuiz.API.Dtos.Questions;

public record CreateObjectiveQuestionDto(string Question, 
    string OptionA, string OptionB, string OptionC, string OptionD, string Answer);
