using BibleQuiz.Domain.Primitives;
using BibleQuiz.Infrastructure.Data;
using BibleQuiz.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BibleQuiz.Infrastructure.Repository;
internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity, new()
{
    private readonly DbSet<TEntity> _db;
    private readonly QuizDbContext _context;

    public GenericRepository(QuizDbContext context)
    {
        _context = context ?? 
            throw new ArgumentNullException(nameof(context));
        _db = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity) =>
        await _db.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) =>
        await _db.AddRangeAsync(entities);

    public void Delete(TEntity entity) =>
        _db.Remove(entity);

    public async Task<List<TEntity>> GetAllAsync() =>
        await _db.ToListAsync();

    public async Task<List<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> spec) =>
        await ApplySpec(spec).ToListAsync();

    public async Task<TEntity> GetAsync(ISpecification<TEntity> spec) =>
        await ApplySpec(spec).FirstOrDefaultAsync();

    public void Update(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    private IQueryable<TEntity> ApplySpec(ISpecification<TEntity> spec) =>
        SpecificationEvaluator.GetQuery(spec, _db.AsQueryable());
}
