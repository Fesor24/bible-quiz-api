using System.ComponentModel.DataAnnotations;

namespace BibleQuiz.Core
{
	public class CreateQuestionsApiModel
	{
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
