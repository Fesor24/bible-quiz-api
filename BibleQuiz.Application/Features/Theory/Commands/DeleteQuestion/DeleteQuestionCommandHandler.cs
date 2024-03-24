using BibleQuiz.Application.Errors;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using BibleQuiz.Domain.Specifications;
using MediatR;
using TestQuestions = BibleQuiz.Domain.Entities.Theory;

namespace BibleQuiz.Application.Features.Theory.Commands.DeleteQuestion;
internal sealed class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteQuestionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit, Error>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetTestQuestionByIdSpecification(request.Id);

        var testQuestion = await _unitOfWork.Repository<TestQuestions>().GetAsync(spec);

        if (testQuestion is null)
            return ApplicationError.TestQuestion.NotFound(request.Id);

        _unitOfWork.Repository<TestQuestions>().Delete(testQuestion);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
