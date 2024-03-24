using TestQuestions = BibleQuiz.Domain.Entities.Theory;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.CreateQuestion;
internal sealed class CreateQuestionCommandHandler :
    IRequestHandler<CreateQuestionCommand, Result<int, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuestionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int, Error>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var res = TestQuestions.Create(request.Question, request.Answer, Domain.Enums.QuestionSource.Author,
            request.Verse);

        if (res.IsFailure)
            return res.Error;

        await _unitOfWork.Repository<TestQuestions>().AddAsync(res.Value);

        await _unitOfWork.Complete();

        return res.Value.Id;
    }
}
