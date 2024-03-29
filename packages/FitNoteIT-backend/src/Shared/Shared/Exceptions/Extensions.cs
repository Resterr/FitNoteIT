﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Shared.Exceptions;

internal static class Extensions
{
	public static IServiceCollection AddErrorHandling(this IServiceCollection services)
	{
		return services.AddScoped<ErrorHandlerMiddleware>();
	}

	public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
	{
		return app.UseMiddleware<ErrorHandlerMiddleware>();
	}
}