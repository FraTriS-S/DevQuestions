using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace DevQuestions.Presenters.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse<TValue>(this Result<TValue, Errors> result)
    {
        return result.IsSuccess
            ? new OkObjectResult(result.Value)
            : GetErrorResult(result.Error);
    }

    private static ObjectResult GetErrorResult(Errors errors)
    {
        if (!errors.Any())
        {
            return new ObjectResult(null) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        var distinctErrorTypes = errors
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        int statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeFromErrorType(distinctErrorTypes[0]);

        return new ObjectResult(errors) { StatusCode = statusCode };
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