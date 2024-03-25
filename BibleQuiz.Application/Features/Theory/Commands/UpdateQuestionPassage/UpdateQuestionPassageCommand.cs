using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.UpdateQuestionPassage;
public record UpdateQuestionPassageCommand(int QuestionId, string Passage) : 
    IRequest<Result<Unit, Error>>;

