using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.UpdateQuestionPassage;
public class UpdateQuestionPassageCommand : IRequest<Result<Unit, Error>>
{
    public UpdateQuestionPassageDto Data { get; set; }
}

public class UpdateQuestionPassageDto
{
    public int QuestionId { get; set; }
    public string Passage { get; set; }
}
