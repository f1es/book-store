using BookStore.Contracts.Applications.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Extensions;

public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult(this ServiceResult serviceResult)
    {
        return serviceResult.IsSuccess
            ? new NoContentResult()
            : ResolveError(serviceResult.ResultType!, serviceResult.Message!);
    }

    public static IActionResult ToActionResult<T>(this ServiceResult<T> serviceResult)
    {
        return serviceResult.IsSuccess
            ? new OkObjectResult(serviceResult.Data)
            : ResolveError(serviceResult.ResultType!, serviceResult.Message!);
    }

    private static IActionResult ResolveError(string resultType, string message)
    {
        var responseObject = new 
        { 
            type = resultType, 
            message = message 
        };

        return resultType switch
        {
            ResultTypes.NotFound => new NotFoundObjectResult(responseObject),
            _ => new ObjectResult(responseObject)
            {
                StatusCode = 500
            },
        };
    }
}
