namespace BibleQuiz.Core
{
	public class RevisionQuestionsDataModel
	{
		/// <summary>
		/// The identifier for this question
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The question
		/// </summary>
		public string Question { get; set; }

		/// <summary>
		/// The answer to this questions
		/// </summary>
		public string Answer { get; set; }
	}
}
