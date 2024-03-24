using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.CreateQuestion;
public class CreateQuestionCommand : IRequest<Result<int, Error>>
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Verse { get; set; }
}
