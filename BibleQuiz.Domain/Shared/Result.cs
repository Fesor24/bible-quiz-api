namespace BibleQuiz.Domain.Shared;
public class Result
{
    protected internal Result(bool isSucccess, Error error)
    {
        IsSuccess = isSucccess;
        Error = error;
    }

    protected internal Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get;}

    public static Result Success => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);


}
