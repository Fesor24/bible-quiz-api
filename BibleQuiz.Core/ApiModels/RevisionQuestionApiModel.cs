using System.ComponentModel.DataAnnotations;

namespace BibleQuiz.Core
{
	public class RevisionQuestionApiModel
	{
		/// <summary>
		/// The identifier for this question
		/// </summary>
		[Required]
		public int Id { get; set; }

		/// <summary>
		/// The question
		/// </summary>
		[Required]
		public string Question { get; set; }

		/// <summary>
		/// The answer to this questions
		/// </summary>
		[Required]
		public string Answer { get; set; }
	}
}
