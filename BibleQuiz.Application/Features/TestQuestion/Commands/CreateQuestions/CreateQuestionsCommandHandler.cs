using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;
using TestQuestions = BibleQuiz.Domain.Entities.TestQuestion;

namespace BibleQuiz.Application.Features.TestQuestion.Commands.CreateQuestion;
internal sealed class CreateQuestionsCommandHandler : IRequestHandler<CreateQuestionsCommand, Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuestionsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit, Error>> Handle(CreateQuestionsCommand request, CancellationToken cancellationToken)
    {
        List<TestQuestions> questions = new();

        foreach(var question in request.Questions)
        {
            var res = TestQuestions.Create(question.Question, question.Answer, 
                Domain.Enums.QuestionSource.Author, question.Verse);

            if (res.IsFailure)
                return res.Error;

            questions.Add(res.Value);
        }

        await _unitOfWork.Repository<TestQuestions>().AddRangeAsync(questions);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
