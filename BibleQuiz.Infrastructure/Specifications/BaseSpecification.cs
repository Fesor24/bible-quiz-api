using BibleQuiz.Domain.Primitives;
using System.Linq.Expressions;

namespace BibleQuiz.Infrastructure.Specifications;
public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity: Entity
{
    public BaseSpecification()
    {
        
    }

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>> Criteria { get; private set; }

    public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

    protected void SetOrderByDesc(Expression<Func<TEntity, object>> orderByDesc) =>
        OrderByDesc = orderByDesc;
}
