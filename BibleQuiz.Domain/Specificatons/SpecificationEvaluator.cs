﻿using BibleQuiz.Domain.Primitives;

namespace BibleQuiz.Domain.Specifications;
public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(ISpecification<TEntity> spec, IQueryable<TEntity> query) 
        where TEntity: Entity
    {
        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (spec.OrderByDesc is not null)
            query = query.OrderByDescending(spec.OrderByDesc);

        if (spec.OrderBy is not null)
            query = query.OrderBy(spec.OrderBy);

        return query;
    }
}
