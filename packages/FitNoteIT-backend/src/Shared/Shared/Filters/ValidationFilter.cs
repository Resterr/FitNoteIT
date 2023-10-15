using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FitNoteIT.Shared.Filters;
public class ValidationFilter : IActionFilter
{
	public void OnActionExecuting(ActionExecutingContext context)
	{
		if (!context.ModelState.IsValid)
		{
			var failures = new List<ValidationFailure>() { };

			foreach (var entry in context.ModelState)
			{
				if (entry.Key != "request")
				{
					foreach (var error in entry.Value.Errors)
					{
						string key;
						string errorMessege;
						if (error.ErrorMessage.Contains("JSON"))
						{
							key = entry.Key.Replace("$.", "");
							errorMessege = "The JSON value could not be converted";
						}
						else
						{
							key = entry.Key;
							errorMessege = error.ErrorMessage;
						}

						var errorModel = new ValidationFailure(key, errorMessege);

						failures.Add(errorModel);
					}
				}
			}

			throw new Exceptions.ValidationException(failures);
		}
	}

	public void OnActionExecuted(ActionExecutedContext context) { }
}
