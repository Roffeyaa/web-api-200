namespace IssueTracker.Api.Middleware;

/// <summary>
/// This exception should be thrown when preconditions are not met within the application.
/// </summary>
/// <param name="message"></param>
public class ChaosException(string message) : ApplicationException(message);
public class GlobalChaosExceptionHandler(
    RequestDelegate next,
    ILogger<GlobalChaosExceptionHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ChaosException ex)
        {
            logger.LogError(ex, "Chaos Exception");

        }
        catch
        {
            logger.LogError("An unexpected error occurred");
            throw;
        }
    }
}