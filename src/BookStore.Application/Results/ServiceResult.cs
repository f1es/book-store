using System.Runtime.CompilerServices;

namespace BookStore.Application.Results;

public record ServiceResult(
    bool IsSuccess,
    string? ResultType,
    string? Message)
{
    public bool IsFailure => IsSuccess is false;
    public static ServiceResult Success() => new ServiceResult(true, null, null);
    public static ServiceResult Failure(string resultType, string message) => new ServiceResult(false, resultType, message);
    public static ServiceResult NotFound(string message) => new ServiceResult(false, ResultTypes.NotFound, message);
}

public record ServiceResult<T>(
    bool IsSuccess,
    string? ResultType,
    string? Message,
    T? Data)
{
    public bool IsFailure => IsSuccess is false;
    public static ServiceResult<T> Success(T data) => new ServiceResult<T>(true, null, null, data);
    public static ServiceResult<T> Failure(string resultType, string message) => new ServiceResult<T>(false, resultType, message, default);
    public static ServiceResult<T> NotFound(string message) => new ServiceResult<T>(false, ResultTypes.NotFound, message, default);

    public static implicit operator ServiceResult<T>(T data) => new ServiceResult<T>(true, null, null, data);
}
