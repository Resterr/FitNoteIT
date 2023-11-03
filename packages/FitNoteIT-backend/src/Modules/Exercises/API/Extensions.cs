using FitNoteIT.Modules.Exercises.API.Requests;
using FitNoteIT.Modules.Exercises.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Exercises.API;

public static class Extensions
{
	public static IServiceCollection AddExercisesModule(this IServiceCollection services)
	{
		services.AddCoreLayer();

		return services;
	}

	public static IApplicationBuilder UseExercisesModule(this IApplicationBuilder app)
	{
		app.UseCoreLayer();

		return app;
	}

	public static WebApplication RegisterExercisesModuleRequests(this WebApplication app)
	{
		app.RegisterExercisesRequests();
		app.RegisterRecordsRequests();

		return app;
	}
}