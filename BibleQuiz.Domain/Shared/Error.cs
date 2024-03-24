namespace BibleQuiz.Domain.Shared;
public class Error
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; }
    public string Message { get; set; }

    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error Validation(string code, string message) => new(code, message);
    public static Error NotFound(string code, string message) => new(code, message);
    public static Error Conflict(string code, string message) => new(code, message);

    public static implicit operator Result(Error error) => Result.Failure(error);
}
