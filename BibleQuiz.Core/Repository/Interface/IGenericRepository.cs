using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleQuiz.Core
{
    /// <summary>
    /// Respository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get a question by a specified condition
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<T> GetQuestionWithSpec(ISpecification<T> spec);
        
        /// <summary>
        /// Get all questions by a specified condition
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetQuestionsAsync(ISpecification<T> spec);
    }
}
