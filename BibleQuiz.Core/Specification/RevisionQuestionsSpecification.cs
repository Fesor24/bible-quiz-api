namespace BibleQuiz.Core
{
	/// <summary>
	/// Revision questions specification
	/// </summary>
	public class RevisionQuestionsSpecification: BaseSpecification<RevisionQuestionsDataModel>
	{
		public RevisionQuestionsSpecification()
		{
			AddOrderBy(x => x.Id);
		}
	}
}
