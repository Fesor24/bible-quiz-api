using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.DeleteQuestion;
public class DeleteQuestionCommand : IRequest<Result<Unit, Error>>
{
    public int Id { get; set; }
}
