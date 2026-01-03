namespace BookStore.Contracts.Applications.Results;

public record ServiceResult(
    bool IsSuccess,
    string? ResultType,
    string? Message)
{
    public static ServiceResult Success() => new ServiceResult(true, null, null);
    public static ServiceResult Failure(string resultType, string message) => new ServiceResult(false, resultType, message);
}

public record ServiceResult<T>(
    bool IsSuccess,
    string? ResultType,
    string? Message,
    T? Data)
{
    public static ServiceResult<T> Success(T data) => new ServiceResult<T>(true, null, null, data);
    public static ServiceResult<T> Failure(string resultType, string message) => new ServiceResult<T>(false, resultType, message, default);

    public static implicit operator ServiceResult<T>(T data) => new ServiceResult<T>(true, null, null, data);
}
