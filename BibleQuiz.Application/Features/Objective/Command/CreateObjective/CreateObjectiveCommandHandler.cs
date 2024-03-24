using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Objective.Command.CreateObjective;
internal sealed class CreateObjectiveCommandHandler : IRequestHandler<CreateObjectiveCommand, Result<int, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateObjectiveCommandHandler(IUnitOfWork unitOfWork) => 
        _unitOfWork = unitOfWork;

    public async Task<Result<int, Error>> Handle(CreateObjectiveCommand request, CancellationToken cancellationToken)
    {
        var res = ObjectiveEntity.Create(request.Question,
            request.OptionA, request.OptionB, request.OptionC, request.OptionD, request.Answer);

        if (res.IsFailure)
            return res.Error;

        await _unitOfWork.Repository<ObjectiveEntity>().AddAsync(res.Value);

        await _unitOfWork.Complete();

        return res.Value.Id;
    }
}
