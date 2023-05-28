namespace BibleQuiz.Core
{
    public class ThousandQuestionsSpecification: BaseSpecification<ThousandQuizQuestionsDataModel>
    {
        public ThousandQuestionsSpecification()
        {
            AddOrderByDescending(x => x.Id);
        }

        public ThousandQuestionsSpecification(int id) : base(x => x.Id == id)
        {

        }
    }
}
