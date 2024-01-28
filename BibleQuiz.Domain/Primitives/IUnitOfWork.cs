namespace BibleQuiz.Domain.Primitives;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity;

    Task<int> Complete();
}
