namespace BibleQuiz.Domain.Primitives;
public interface IGenericRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(ISpecification<TEntity> spec);
    Task<List<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> spec);

}
