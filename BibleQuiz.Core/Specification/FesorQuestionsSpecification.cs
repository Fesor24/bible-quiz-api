namespace BibleQuiz.Core
{
	public class FesorQuestionsSpecification: BaseSpecification<FesorQuestionsDataModel>
	{
		public FesorQuestionsSpecification()
		{
			AddOrderBy(x => x.Id);
		}
	}
}
