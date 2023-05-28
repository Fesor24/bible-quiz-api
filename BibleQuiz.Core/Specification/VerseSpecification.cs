namespace BibleQuiz.Core.Specification
{
    public class VerseSpecification: BaseSpecification<VerseOfTheDayDataModel>
    {
        public VerseSpecification()
        {

        }

        public VerseSpecification(int id): base(x => x.Id == id) { } 
        
    }
}
