namespace OnboardingIntegrationExample.AwesomeShop.Domain.Primitives.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure  => !IsSuccess;
    public Error? Error { get; }

    public Result(bool isSuccess, Error? error = null)
    {
        switch (isSuccess)
        {
            case true when error is not null:
            case false when error is null:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }


    public static Result Success() => new(true);
    public static Result<TValue> Success<TValue>(TValue value) => new(true, value);

    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(false, default!, error);
}