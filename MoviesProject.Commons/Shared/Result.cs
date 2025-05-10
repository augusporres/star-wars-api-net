namespace MoviesProject.Commons.Shared;

public class Result<TValue>
{
    protected internal Result(TValue? value, bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException();
        }
        if (!isSuccess && string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException();
        }
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }
    public TValue? Value { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; set; }

    public static Result<TValue> Success(TValue value) => new(value, true, string.Empty);
    public static Result<TValue> Failure(string error) => new(default, false, error);
}
