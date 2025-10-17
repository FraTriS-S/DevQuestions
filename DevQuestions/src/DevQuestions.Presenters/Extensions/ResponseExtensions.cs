using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace DevQuestions.Presenters.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse<TValue>(this Result<TValue, Failure> result)
    {
        return result.IsSuccess
            ? new OkObjectResult(result.Value)
            : GetErrorResult(result.Error);
    }

    private static ObjectResult GetErrorResult(Failure failure)
    {
        if (!failure.Any())
        {
            return new ObjectResult(null) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        var distinctErrorTypes = failure
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        int statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeFromErrorType(distinctErrorTypes[0]);

        return new ObjectResult(failure) { StatusCode = statusCode };
    }

    private static int GetStatusCodeFromErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.VALIDATION => StatusCodes.Status400BadRequest,
            ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
            ErrorType.CONFLICT => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };
}