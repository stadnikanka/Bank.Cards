using Bank.Cards.Application.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Cards.Api.Extensions;

public static class OperationResultExtensions
{
    public static IActionResult AsActionResult<T>(this OperationResult<T> requestResult)
    {
        if (requestResult.IsSuccess)
        {
            return new OkObjectResult(requestResult.Response);
        }

        return requestResult.ErrorCode switch
        {
            400 => new NotFoundObjectResult(new ProblemDetails { Status = 400, Detail = requestResult.ErrorMessage }),
            404 => new NotFoundObjectResult(new ProblemDetails { Status = 404, Detail = requestResult.ErrorMessage }),
            _ => new BadRequestObjectResult(new ProblemDetails { Status = 400, Detail = requestResult.ErrorMessage })
        };
    }
}