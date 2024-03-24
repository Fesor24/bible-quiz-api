using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.CreatePastQuestions;
public class CreatePastQuestionsCommand : IRequest<Result<Unit, Error>>
{
    public List<PastQuestion> Questions { get; set; } = new();
    public QuestionSource Source { get; set; }
}

public class PastQuestion 
{ 
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty; 
    public string Answer { get; set; } = string.Empty; 
}


