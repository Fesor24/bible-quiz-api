using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Entities;
public class Objective : Entity
{
    private Objective()
    {
        
    }

    private Objective(string question, ObjectiveOptions options, string answer)
    {
        Question = question;
        Options = options;
        Answer = answer;
    }

    public string Question { get; private set; }
    
    public ObjectiveOptions Options { get; private set; }
    public string Answer { get; private set; }

    public static Result<Objective, Error> Create(string question, string optionA, string optionB, 
        string optionC, string optionD, string answer)
    {
        if(string.IsNullOrWhiteSpace(answer) || string.IsNullOrWhiteSpace(question) ||
            string.IsNullOrWhiteSpace(optionD) || string.IsNullOrWhiteSpace(optionC)
            || string.IsNullOrWhiteSpace(optionB) || string.IsNullOrWhiteSpace(optionA))
        {
            return Error.Validation("Invalid.Property", 
                "One or more properties(question, answer or the various options) are empty or null");
        }

        Objective obj = new(question, new ObjectiveOptions(optionA, optionB, optionC, optionD), answer);

        return obj;
    }
}
