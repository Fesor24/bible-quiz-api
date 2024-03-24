using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Objective.Command.CreateObjective;
public record CreateObjectiveCommand(
    string Question,
    string OptionA,
    string OptionB,
    string OptionC,
    string OptionD,
    string Answer
    ) : IRequest<Result<int, Error>>;
