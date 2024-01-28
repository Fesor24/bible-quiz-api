using BibleQuiz.Domain.Primitives;
using BibleQuiz.Infrastructure.Data;
using System.Collections;

namespace BibleQuiz.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly QuizDbContext _context;
    private Hashtable _repos;

    public UnitOfWork(QuizDbContext context)
    {
        _context = context;
    }

    public async Task<int> Complete() => 
        await _context.SaveChangesAsync();

    public void Dispose() =>
        _context.Dispose();

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity
    {
        _repos ??= new Hashtable();

        var key = typeof(TEntity).FullName;

        if (!_repos.ContainsKey(key))
        {
            var genericType = typeof(GenericRepository<>);

            var repositoryInstance = Activator.CreateInstance(
                genericType.MakeGenericType(typeof(TEntity)), _context);

            _repos.Add(key, repositoryInstance);
        }

        return (IGenericRepository<TEntity>) _repos[key];
    }
}
