using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Shared;
using MediatR;
using TestQuestions = BibleQuiz.Domain.Entities.TestQuestion;

namespace BibleQuiz.Application.Features.TestQuestion.Commands.CreatePastQuestions;
public class CreatePastQuestionsCommand : IRequest<Result<List<TestQuestions>, Error>>
{
    public List<CreatePastQuestionsDto> Questions { get; set; }
    public QuestionSource Source { get; set; }
}

public class CreatePastQuestionsDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
}
