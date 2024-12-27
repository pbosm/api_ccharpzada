using System.Net;
using Newtonsoft.Json;

namespace SystemCcharpzinho.API.Middleware;

public class ErrorReponseMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorReponseMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string message = exception.Message;

        switch (exception)
        {
            case KeyNotFoundException _:
                status = HttpStatusCode.NotFound;
                break;
            default:
                status = HttpStatusCode.InternalServerError;
                break;
        }

        var response = new { status = status, message = message };
        var payload = JsonConvert.SerializeObject(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(payload);
    }
}