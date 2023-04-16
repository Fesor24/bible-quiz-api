namespace BibleQuiz.Core
{
	/// <summary>
	/// Revision questions specification
	/// </summary>
	public class RevisionQuestionsSpecification: BaseSpecification<RevisionQuestionsDataModel>
	{
		public RevisionQuestionsSpecification(string userId): base(x => x.UserId == userId)
		{
			AddOrderBy(x => x.Id);
		}
	}
}
