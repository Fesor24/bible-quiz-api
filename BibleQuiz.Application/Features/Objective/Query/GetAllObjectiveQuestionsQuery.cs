using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Objective.Query;
public record GetAllObjectiveQuestionsQuery : 
    IRequest<Result<IReadOnlyList<ObjectiveQuestionResponse>, Error>>;
