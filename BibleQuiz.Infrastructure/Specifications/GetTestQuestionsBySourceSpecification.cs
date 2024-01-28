using BibleQuiz.Domain.Entities;
using BibleQuiz.Domain.Enums;

namespace BibleQuiz.Infrastructure.Specifications;
public class GetTestQuestionsBySourceSpecification : BaseSpecification<TestQuestion>
{
    public GetTestQuestionsBySourceSpecification(QuestionSource source) : base(x => x.Source == source)
    {
    }
}
