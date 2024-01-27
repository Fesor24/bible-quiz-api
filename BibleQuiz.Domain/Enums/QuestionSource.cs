using System.Runtime.Serialization;

namespace BibleQuiz.Domain.Enums;
public enum QuestionSource
{
    [EnumMember(Value ="Unknown")]
    Unknown,
    [EnumMember(Value ="BibleQuizzes")]
    BibleQuizzes,
    [EnumMember(Value = "Author")]
    Author
}
