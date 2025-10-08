namespace Common.Models.Results;

/// <summary>
/// Represents the result of an operation without a return value
/// </summary>
public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; protected set; } = string.Empty;
    public List<string> Errors { get; protected set; } = new();

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("A successful result cannot have an error");

        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("A failed result must have an error");

        IsSuccess = isSuccess;
        Error = error;
    }

    protected Result(bool isSuccess, List<string> errors)
    {
        if (isSuccess && errors.Any())
            throw new InvalidOperationException("A successful result cannot have errors");

        if (!isSuccess && !errors.Any())
            throw new InvalidOperationException("A failed result must have at least one error");

        IsSuccess = isSuccess;
        Errors = errors;
        Error = string.Join("; ", errors);
    }

    public static Result Success() => new(true, string.Empty);

    public static Result Failure(string error) => new(false, error);

    public static Result Failure(List<string> errors) => new(false, errors);
}

/// <summary>
/// Represents the result of an operation with a return value
/// </summary>
public class Result<T> : Result
{
    public T? Value { get; protected set; }

    protected internal Result(T? value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    protected internal Result(T? value, bool isSuccess, List<string> errors)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true, string.Empty);

    public static new Result<T> Failure(string error) => new(default, false, error);

    public static new Result<T> Failure(List<string> errors) => new(default, false, errors);
}
