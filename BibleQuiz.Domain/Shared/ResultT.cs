namespace BibleQuiz.Domain.Shared;
public class Result<TValue, TError> : Result where TError : Error
{
    private readonly TValue _value;
    private readonly TError _error;

    public Result(TValue value) : base()
    {
        _value = value;
    }

    public Result(TError error, bool isSuccess) : base(isSuccess, error)
    {
        _error = error;
    }

    public TValue Value => IsSuccess ? _value :
        throw new Exception("Value can not be accessed");

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error, false);

    public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> error) =>
        IsSuccess ? success(_value) : error(_error);
}
