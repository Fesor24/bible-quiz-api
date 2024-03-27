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

        if (answer.Length > 1)
            return Error.Validation("Invalid.Answer", "Answer can not be more than one character");

        string[] acceptableAnswers = {"A", "B", "C", "D"};

        if (!acceptableAnswers.Any(x => x.Equals(answer, StringComparison.InvariantCultureIgnoreCase)))
            return Error.Validation("Invalid.AnswerOption", $"Acceptable options are {string.Join(",", acceptableAnswers)}");

        answer = answer.ToUpper();

        Objective obj = new(question, new ObjectiveOptions(optionA, optionB, optionC, optionD), answer);

        return obj;
    }
}
