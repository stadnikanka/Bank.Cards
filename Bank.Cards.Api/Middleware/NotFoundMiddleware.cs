using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Bank.Cards.Api.Middleware;

public class NotFoundMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response is { StatusCode: StatusCodes.Status404NotFound, HasStarted: false })
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Detail = "ResourceNotFound",
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}