using System.Text.Json;

namespace BookStore.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(ex, context);
        }
    }

    private async Task HandleException(Exception ex, HttpContext context)
    {
        var statusCode = 500;
        var message = ex.Message;

        var exceptionResult = JsonSerializer.Serialize(new 
        { 
            code = statusCode,
            error = message,
            exceptionType = ex.GetType().ToString(),
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(exceptionResult);
    }
}
