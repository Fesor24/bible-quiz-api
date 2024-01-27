using BibleQuiz.Domain.Errors;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Entities;
public sealed class Verse : Entity
{
    public string Title { get; private set; }
    public string Passage { get; private set; }

    public Verse()
    {
        
    }

    public Verse(string title, string passage)
    {
        Title = title;
        Passage = passage;
    }

    public static Result<Verse, Error> Create(string title, string passage)
    {
        if (string.IsNullOrWhiteSpace(title))
            return DomainErrors.Verse.InvalidTitle;

        if (string.IsNullOrWhiteSpace(passage))
            return DomainErrors.Verse.InvalidPassage;

        Verse verse = new(title, passage);

        return verse;
    } 
}
