using BibleQuiz.Domain.Primitives;

namespace BibleQuiz.Infrastructure.Specifications;
internal static class SpecificationEvaluator
{
    internal static IQueryable<TEntity> GetQuery<TEntity>(ISpecification<TEntity> spec, IQueryable<TEntity> query) 
        where TEntity: Entity
    {
        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (spec.OrderByDesc is not null)
            query = query.OrderByDescending(spec.OrderByDesc);

        return query;
    }
}
