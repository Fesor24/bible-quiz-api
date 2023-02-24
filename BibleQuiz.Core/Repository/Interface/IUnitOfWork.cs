namespace BibleQuiz.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity: class;

        Task<int> Complete();
    }
}
