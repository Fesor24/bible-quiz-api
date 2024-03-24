using BibleQuiz.Application.Features.Theory.Queries.GetQuestions;
using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Queries.GetQuestionsBySource;
public class GetQuestionsBySourceRequest : IRequest<Result<IReadOnlyList<GetQuestionResponse>, Error>>
{
    public QuestionSource Source { get; set; }
}
