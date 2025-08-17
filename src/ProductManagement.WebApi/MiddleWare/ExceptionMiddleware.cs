using ProductManagement.Core.Exceptions;

namespace ProductManagement.WebApi.MiddleWare;

public class ExceptionMiddleWare {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleWare> _logger;

    public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger)
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
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred" });
        }
    }
}

