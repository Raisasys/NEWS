using Core;

public class AppExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public AppExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
			//await RedirectIfExistAnyIssue(httpContext);
		}
		catch (DomainException domainException)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 500;
            await context.Response.WriteAsync(domainException.ToString());
			await context.Response.CompleteAsync();
		}
		catch (CoreException coreException)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 500;
			await context.Response.WriteAsync(coreException.ToString());
			await context.Response.CompleteAsync();
		}
		catch (Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 500;
			await context.Response.WriteAsync(exception.ToString());
			await context.Response.CompleteAsync();
		}
		/*catch (Exception ex)
		{
			throw;
		}*/
	}

}