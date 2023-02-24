using System.Linq.Expressions;

namespace BibleQuiz.Core
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
            
        }

        /// <summary>
        /// Criteria property
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Includes property for navigation props
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = 
            new List<Expression<Func<T, object>>>();

        /// <summary>
        /// Order by descending property
        /// </summary>
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        /// <summary>
        /// Method to add include expression to the Includes list
        /// </summary>
        /// <param name="include"></param>
        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        /// <summary>
        /// Method to set our OrderByDescending expressions
        /// </summary>
        /// <param name="orderByDesc"></param>
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }
    }
}
