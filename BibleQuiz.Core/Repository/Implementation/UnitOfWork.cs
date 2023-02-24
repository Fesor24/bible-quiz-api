using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleQuiz.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Scoped instace of AppDbContext
        /// </summary>
        private readonly ApplicationDbContext context;

        private Hashtable repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            // We check if we have something in the hashtable
            if (repositories == null)
            {
                // We create an instance of it if is is null
                repositories = new Hashtable();            
            }

            // We get the name of the type of TEntity
            var type = typeof(TEntity).Name;

            // We check if the hastable contains the entry with the specific name
            if (!repositories.ContainsKey(type))
            {
                // If it does not contain it
                // We create a type of our GenricRepository of TEntity
                var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));

                // We create an instance of it
                var repositoryInstance = Activator.CreateInstance(repositoryType, context);

                // We add it to the hashtable
                repositories.Add(type, repositoryInstance);
            };

            return (IGenericRepository<TEntity>) repositories[type];
        }
    }
}
