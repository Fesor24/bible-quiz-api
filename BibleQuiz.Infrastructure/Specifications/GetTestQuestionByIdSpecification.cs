using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Infrastructure.Specifications;
public sealed class GetTestQuestionByIdSpecification : BaseSpecification<TestQuestion>
{
    public GetTestQuestionByIdSpecification(int id) : base(x => x.Id == id)
    {
        
    }
}
