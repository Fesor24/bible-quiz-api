using BibleQuiz.Domain.Primitives;
using System.Linq.Expressions;

namespace BibleQuiz.Domain.Specifications;
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

    public Expression<Func<TEntity, object>> OrderBy { get; private set; }

    protected void SetOrderByDesc(Expression<Func<TEntity, object>> orderByDesc) =>
        OrderByDesc = orderByDesc;

    protected void SetOrderBy(Expression<Func<TEntity, object>> orderBy) =>
        OrderBy = orderBy;
}
