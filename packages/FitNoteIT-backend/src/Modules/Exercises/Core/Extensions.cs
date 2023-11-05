using System.Reflection;
using FitNoteIT.Modules.Exercises.Core.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Exercises.Core;

public static class Extensions
{
	public static IServiceCollection AddCoreLayer(this IServiceCollection services)
	{
		services.AddPersistence();

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