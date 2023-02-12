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
            dbSet = context.Set<T>();  
        }
        public async Task<IReadOnlyList<T>> GetAllQuestionsAsync()
        {
            // Fetch all details
            var result = await dbSet.ToListAsync();

            // Return the result
            return result;
        }

        public async Task<T> GetQuestionByIdAsync(int id)
        {
            var result = await dbSet.FindAsync(id);

            return result;
        }
    }
}
