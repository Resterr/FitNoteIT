using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace FitNoteIT.Shared.Exceptions;
internal sealed class ErrorHandlingMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (CustomException ex)
		{
			context.Response.StatusCode = (int)ex.StatusCode;
			context.Response.Headers.Add("content-type", "application/json");

			var response = new
            {
                ex.Title,
                Detail = ex.Message,
                Errors = GetErrors(ex)
            };

			var json = JsonSerializer.Serialize(response);
			await context.Response.WriteAsync(json);
		}
		catch (Exception)
        {
            context.Response.StatusCode = 500;
            context.Response.Headers.Add("content-type", "application/json");

            var response = new
            {
                Title = "Internal Server Error",
                Detail = "Something went wrong"
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
	}

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;
        if (exception is ValidationException validationException)
        {
            errors = validationException.ErrorsDictionary;
        }
        return errors;
    }
}
