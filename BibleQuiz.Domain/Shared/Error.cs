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

    public static ValidationError Validation(string code, string message) => new(code, message);
    public static NotFoundError NotFound(string code, string message) => new(code, message);

    public static ConflictError Conflict(string code, string message) => new(code, message);

    public static bool IsNotFound { get; private set; }
    public static bool IsValidation { get; private set; }
    public static bool IsConflict { get; private set; }

    public static implicit operator Result(Error error) => Result.Failure(error);
}

public class ValidationError : Error
{
    public ValidationError(string code, string message): base(code, message)
    {
        
    }
}

public class NotFoundError: Error
{
    public NotFoundError(string code, string message) : base(code, message)
    {
        
    }
}

public class ConflictError : Error
{
    public ConflictError(string code, string message) : base(code, message)
    {

    }
}
