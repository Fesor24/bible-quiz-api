using Microsoft.EntityFrameworkCore;

namespace BibleQuiz.Core
{
    /// <summary>
    /// This class evaluates the specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpecificationEvaluator<T> where T: class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            // If there is a where expression
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            // If there is an order by expression
            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // Aggregating the Includes list
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Return query
            return query;
        }
    }
}
