using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Core.Persistence.Seeders;
using FitNoteIT.Shared.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Exercises.Core.Persistence;

internal static class Extensions
{
	public static IServiceCollection AddPersistence(this IServiceCollection services)
	{
		services.AddSqlServer<ExercisesDbContext>();
		services.AddScoped<IExercisesDbContext>(provider => provider.GetRequiredService<ExercisesDbContext>());
		services.AddScoped<IExercisesSeeder, ExercisesSeeder>();

		return services;
	}

	public static IApplicationBuilder SeedUsersData(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
			.CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<ExercisesDbContext>();
		if (context.Database.GetPendingMigrations()
			.Any())
			return app;

		var usersSeeder = scope.ServiceProvider.GetRequiredService<IExercisesSeeder>();

		usersSeeder.SeedCategories();
		usersSeeder.SeedExercises();

		return app;
	}
}