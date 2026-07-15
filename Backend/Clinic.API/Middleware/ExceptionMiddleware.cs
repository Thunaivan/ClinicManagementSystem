using System.Net;
using System.Text.Json;
using Clinic.API.Models;
using Clinic.Application.Common.Exceptions;
namespace Clinic.API.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, ex.Message);
            _logger.LogError(
     ex,
     "Unhandled exception occurred. TraceId: {TraceId}",
     context.TraceIdentifier);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        HttpStatusCode statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,

            ValidationException => HttpStatusCode.BadRequest,

            BusinessException => HttpStatusCode.BadRequest,

            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        /*var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = exception.Message,
            TraceId = context.TraceIdentifier
        };*/
        var message = statusCode == HttpStatusCode.InternalServerError
    ? "An unexpected error occurred."
    : exception.Message;
        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message,
            TraceId = context.TraceIdentifier
        };
        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}