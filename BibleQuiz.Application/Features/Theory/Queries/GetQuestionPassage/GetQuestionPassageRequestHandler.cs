using TestQuestionEntity = BibleQuiz.Domain.Entities.Theory;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using BibleQuiz.Domain.Specifications;
using MediatR;
using BibleQuiz.Application.Errors;

namespace BibleQuiz.Application.Features.Theory.Queries.GetQuestionPassage;
internal sealed class GetQuestionPassageRequestHandler : IRequestHandler<GetQuestionPassageRequest,
    Result<GetQuestionPassageResponse, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBibleService _bibleService;

    public GetQuestionPassageRequestHandler(IUnitOfWork unitOfWork, IBibleService bibleService)
    {
        _unitOfWork = unitOfWork;
        _bibleService = bibleService;
    }

    public async Task<Result<GetQuestionPassageResponse, Error>> Handle(GetQuestionPassageRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new GetTestQuestionByIdSpecification(request.QuestionId);

        var question = await _unitOfWork.Repository<TestQuestionEntity>().GetAsync(spec);

        if (question is null)
            return ApplicationError.TestQuestion.NotFound(request.QuestionId);

        if (!string.IsNullOrWhiteSpace(question.Passage))
            return new GetQuestionPassageResponse(question.Passage, question.Id);

        var res = await _bibleService.GetScripturesAsync(new Domain.Models.BibleVerse
        {
            Verse = question.Verse
        });

        if (res.IsSuccess && res.Value.Data.Passages.Count > 0)
        {
            string scripture = res.Value.Data.Passages[0].Content;

            var resQuestion = question.UpdatePassage(scripture);

            if (resQuestion.IsSuccess)
            {
                _unitOfWork.Repository<TestQuestionEntity>().Update(resQuestion.Value);

                await _unitOfWork.Complete();
            }

            return new GetQuestionPassageResponse(res.Value.Data.Passages[0].Content, question.Id);
        }

        return new GetQuestionPassageResponse("Unable to get scripture at the moment", question.Id);
    }
}
