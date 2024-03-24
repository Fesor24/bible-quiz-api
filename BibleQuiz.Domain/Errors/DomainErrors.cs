using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Errors;
internal static class DomainErrors
{
    internal static class Question
    {
        internal static Error InvalidQuestion = Error.Validation("Question.Empty", "Question can not be null or empty");
        internal static Error InvalidAnswer = Error.Validation("Answer.Empty", "Answer can not be null or empty");
        internal static Error InvalidPassage = Error.Validation("Passage.Empty", "Passage can not be null or empty");
    }

    internal static class Verse
    {
        internal static Error InvalidTitle = Error.Validation("Title.Empty", "Title can not be null or empty");
        internal static Error InvalidPassage = Error.Validation("Passage.Empty", "Passage can not be null or empty");
    }
}
