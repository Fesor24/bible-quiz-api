using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Domain.Specifications;
public sealed class GetTestQuestionByIdSpecification : BaseSpecification<Theory>
{
    public GetTestQuestionByIdSpecification(int id) : base(x => x.Id == id)
    {
        
    }
}
