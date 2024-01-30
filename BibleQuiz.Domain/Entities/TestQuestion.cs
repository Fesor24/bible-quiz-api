using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Errors;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Entities;
public sealed class TestQuestion : Entity
{
    public string Question { get; private set; }
    public string Answer { get; private set; }
    public string Verse { get; private set; }
    public QuestionSource Source { get; private set; }
    public string Passage { get; private set; }

    public TestQuestion()
    {
        
    }

    public TestQuestion(string question, string answer, QuestionSource source, string verse)
    {
        Question = question;
        Answer = answer;
        Source = source;
        Verse = verse;
    }

    public static Result<TestQuestion, Error> Create(string question, string answer, 
        QuestionSource source, string verse)
    {
        if (string.IsNullOrWhiteSpace(question))
            return DomainErrors.TestQuestion.InvalidQuestion;

        if(string.IsNullOrWhiteSpace(answer))
            return DomainErrors.TestQuestion.InvalidAnswer;

        TestQuestion questions = new(question, answer, source, verse);

        return questions;
    }

    public Result<TestQuestion, Error> Update(string question, string answer)
    {
        if (string.IsNullOrWhiteSpace(question))
            return DomainErrors.TestQuestion.InvalidQuestion;

        if (string.IsNullOrWhiteSpace(answer))
            return DomainErrors.TestQuestion.InvalidAnswer;

        Answer = answer;
        Question = question;

        return this;
    }

    public Result<TestQuestion, Error> UpdatePassage(string passage)
    {
        if (string.IsNullOrWhiteSpace(passage))
            return DomainErrors.TestQuestion.InvalidPassage;

        Passage = passage;

        return this;
    }
}
