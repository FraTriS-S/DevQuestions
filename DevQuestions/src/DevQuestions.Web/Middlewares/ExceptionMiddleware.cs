using System.Text.Json;
using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        var (code, errors) = exception switch
        {
            BadRequestException =>
                (StatusCodes.Status500InternalServerError, JsonSerializer.Deserialize<Error[]>(exception.Message)),
            NotFoundException =>
                (StatusCodes.Status404NotFound, JsonSerializer.Deserialize<Error[]>(exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, [Error.Failure(null, "Something went wrong")])
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = code;

        await httpContext.Response.WriteAsJsonAsync(errors);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app) =>
        app.UseMiddleware<ExceptionMiddleware>();
}