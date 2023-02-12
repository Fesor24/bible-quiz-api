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
        /// Get question by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetQuestionByIdAsync(int id);

        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAllQuestionsAsync();
    }
}
