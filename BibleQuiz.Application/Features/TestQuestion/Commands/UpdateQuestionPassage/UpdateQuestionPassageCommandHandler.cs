using TestQuestionEntity = BibleQuiz.Domain.Entities.TestQuestion;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using BibleQuiz.Infrastructure.Specifications;
using MediatR;
using BibleQuiz.Application.Errors;

namespace BibleQuiz.Application.Features.TestQuestion.Commands.UpdateQuestionPassage;
internal sealed class UpdateQuestionPassageCommandHandler : 
    IRequestHandler<UpdateQuestionPassageCommand, Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuestionPassageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit, Error>> Handle(UpdateQuestionPassageCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetTestQuestionByIdSpecification(request.Data.QuestionId);

        var question = await _unitOfWork.Repository<TestQuestionEntity>().GetAsync(spec);

        if(question is null)
            return ApplicationError.TestQuestion.NotFound(request.Data.QuestionId);

        var res = question.UpdatePassage(request.Data.Passage);

        if (res.IsFailure)
            return res.Error;

        _unitOfWork.Repository<TestQuestionEntity>().Update(res.Value);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
