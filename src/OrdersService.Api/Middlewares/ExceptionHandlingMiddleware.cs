using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace OrdersService.Api.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context); // Chama o próximo middleware na pipeline
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Define o código de status 500

        var problemDetails = new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Title = "Internal Server Error",
            Detail = exception.Message // Você pode personalizar isso conforme necessário
        };

        return context.Response.WriteAsJsonAsync(problemDetails);
    }
}
