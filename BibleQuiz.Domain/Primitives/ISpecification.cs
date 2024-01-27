using System.Linq.Expressions;

namespace BibleQuiz.Domain.Primitives;
public interface ISpecification<TEntity> where TEntity : Entity
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    Expression<Func<TEntity, object>> OrderByDesc { get; }
}
