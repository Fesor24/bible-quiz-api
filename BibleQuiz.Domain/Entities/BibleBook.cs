using BibleQuiz.Domain.Primitives;

namespace BibleQuiz.Domain.Entities;
public sealed class BibleBook : Entity
{
    public string BibleId { get; private set; }

    public string ShortName { get; private set; }

    public string NormalizedShortName { get; private set; }

    public string LongName { get; private set; }

    public BibleBook()
    {
            
    }

    public BibleBook(string bibleId, string shortName, string normalizedShortName, string longName)
    {
        BibleId = bibleId;
        ShortName = shortName;
        NormalizedShortName = normalizedShortName;
        LongName = longName;
    }
}
