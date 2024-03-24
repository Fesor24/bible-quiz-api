using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Errors;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Entities;
public sealed class Theory : Entity
{
    public string Question { get; private set; }
    public string Answer { get; private set; }
    public string Verse { get; private set; }
    public QuestionSource Source { get; private set; }
    public string Passage { get; private set; }

    private Theory()
    {
        
    }

    public Theory(string question, string answer, QuestionSource source, string verse)
    {
        Question = question;
        Answer = answer;
        Source = source;
        Verse = verse;
    }

    public static Result<Theory, Error> Create(string question, string answer, 
        QuestionSource source, string verse)
    {
        if (string.IsNullOrWhiteSpace(question))
            return DomainErrors.Question.InvalidQuestion;

        if (string.IsNullOrWhiteSpace(answer))
            return DomainErrors.Question.InvalidAnswer;

        Theory questions = new(question, answer, source, verse);

        return questions;
    }

    public Result<Theory, Error> Update(string question, string answer)
    {
        if (string.IsNullOrWhiteSpace(question))
            return DomainErrors.Question.InvalidQuestion;

        if (string.IsNullOrWhiteSpace(answer))
            return DomainErrors.Question.InvalidAnswer;

        Answer = answer;
        Question = question;

        return this;
    }

    public Result<Theory, Error> UpdatePassage(string passage)
    {
        if (string.IsNullOrWhiteSpace(passage))
            return DomainErrors.Question.InvalidPassage;

        Passage = passage;

        return this;
    }
}
