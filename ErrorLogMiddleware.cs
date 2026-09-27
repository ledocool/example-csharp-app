using System;

namespace example_csharp_app;

public class ErrorLogMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IConfiguration configuration)
    {
        try 
        {
            await _next(httpContext);
            return;
        } 
        catch (Exception ex)
        {
            // todo: logme
            Console.WriteLine(ex.Message);
            httpContext.Response.StatusCode = 500;
        }
    }
}
