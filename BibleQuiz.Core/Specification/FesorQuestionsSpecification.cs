namespace BibleQuiz.Core
{
	public class FesorQuestionsSpecification: BaseSpecification<FesorQuestionsDataModel>
	{
		public FesorQuestionsSpecification()
		{
			AddOrderBy(x => x.Id);
		}

		public FesorQuestionsSpecification(int id) : base(x => x.Id == id) { }
	}
}
