using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Domain.Specifications;
public sealed class GetTestQuestionByIdSpecification : BaseSpecification<TestQuestion>
{
    public GetTestQuestionByIdSpecification(int id) : base(x => x.Id == id)
    {
        
    }
}
