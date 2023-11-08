using System.Reflection;
using FitNoteIT.Modules.Exercises.Core.Persistence;
using FitNoteIT.Modules.Exercises.Core.Services;
using FitNoteIT.Modules.Exercises.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Exercises.Core;

public static class Extensions
{
	public static IServiceCollection AddCoreLayer(this IServiceCollection services)
	{
		services.AddPersistence();
		services.AddTransient<IExercisesModuleApi, ExercisesModuleApi>();

		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		return services;
	}

	public static IApplicationBuilder UseCoreLayer(this IApplicationBuilder app)
	{
		app.SeedUsersData();

		return app;
	}
}