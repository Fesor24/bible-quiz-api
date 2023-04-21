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
        Task<List<T>> GetQuestionsAsync(ISpecification<T> spec);

        /// <summary>
        /// To add question to the table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddQuestions(T entity);

        /// <summary>
        /// To add multiple questions to the db
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddQuestionRange(List<T> entities);

        /// <summary>
        /// To delete multiple questions
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void DeleteQuestionsRange(List<T> entities);

		/// <summary>
		/// To delete multiple questions
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		void DeleteQuestion(T entity);
	}
}
