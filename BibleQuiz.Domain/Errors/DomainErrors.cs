using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Errors;
internal static class DomainErrors
{
    internal static class TestQuestion
    {
        internal static Error InvalidQuestion = new("400", "Invalid question");

        internal static Error InvalidAnswer = new("400", "Invalid answer");
        internal static Error InvalidPassage = new("400", "Invalid passage");
    }

    internal static class Verse
    {
        internal static Error InvalidTitle = new("400", "Invalid title");
        internal static Error InvalidPassage = new("400", "Invalid passage");
    }
}
