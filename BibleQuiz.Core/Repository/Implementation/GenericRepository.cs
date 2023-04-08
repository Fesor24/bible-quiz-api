using Microsoft.EntityFrameworkCore;

namespace BibleQuiz.Core
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Scoped instance of app db context
        /// </summary>
        private readonly ApplicationDbContext context;

        private readonly DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();  
        }

        /// <summary>
        /// Add list of questions to db
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
		public async Task AddQuestionRange(List<T> entities) => await dbSet.AddRangeAsync(entities);

		/// <summary>
		/// To add questions to the db
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task AddQuestions(T entity) => await dbSet.AddAsync(entity);
		

		/// <summary>
		/// Fetch a list based on the specified specification
		/// </summary>
		/// <param name="spec"></param>
		/// <returns></returns>
		public async Task<IReadOnlyList<T>> GetQuestionsAsync(ISpecification<T> spec) => await ApplySpecification(spec).ToListAsync();
        

        /// <summary>
        /// Fetch a question based on the specified specification
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<T> GetQuestionWithSpec(ISpecification<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();


		/// <summary>
		/// Private method to apply the specification
		/// </summary>
		/// <param name="spec"></param>
		/// <returns></returns>
		private IQueryable<T> ApplySpecification(ISpecification<T> spec) => SpecificationEvaluator<T>.GetQuery(dbSet.AsQueryable(), spec);
        
    }
}
