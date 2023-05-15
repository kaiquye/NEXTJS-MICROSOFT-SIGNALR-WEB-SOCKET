using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebSocket.Domain.Error;

namespace WebSocket.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var error = new AppError
            {
                message = badRequestException.Message,
                date = new DateTime()
            };
            await context.Response.WriteAsJsonAsync(error);
        }
        catch (NotFoundException notFoundExceptions)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var error = new AppError
            {
                message = notFoundExceptions.Message,
                date = new DateTime()
            };
            await context.Response.WriteAsJsonAsync(error);
        }
        catch (ConflictException conflictException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            var error = new AppError
            {
                message = conflictException.Message,
                date = new DateTime()
            };
            await context.Response.WriteAsJsonAsync(error);
        }
        catch (Exception exception)
        {
            Console.WriteLine("[INTERNAL-ERROR]" + exception.Message);
            Console.WriteLine("[INTERNAL-ERROR]" + exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = new AppError
            {
                message = "internal error on the server. contact an admin.",
                date = new DateTime()
            };
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}