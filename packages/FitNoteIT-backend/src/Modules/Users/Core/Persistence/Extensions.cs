using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Persistence.Repositories;
using FitNoteIT.Modules.Users.Core.Persistence.Seeders;
using FitNoteIT.Shared.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Users.Core.Persistence;
internal static class Extensions
{
	public static IServiceCollection AddPersistence(this IServiceCollection services)
	{
		services.AddSqlServer<UsersDbContext>();
		services.AddScoped<IUsersSeeder, UsersSeeder>();
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}

	public static IApplicationBuilder SeedUsersData(this IApplicationBuilder app)
	{
		using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			using var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
			if (!context.Database.GetPendingMigrations().Any())
			{
				var usersSeeder = scope.ServiceProvider.GetRequiredService<IUsersSeeder>();

				if (!context.Roles.Any())
				{
					context.Roles.AddRange(usersSeeder.SeedDefaultRoles());
					context.SaveChanges();
				}

				if (!context.Users.Any())
				{
					var superAdmin = usersSeeder.SeedSuperAdmin();
					var role = context.Roles.Single(x => x.Name == "SuperAdmin");
					superAdmin.AddRole(role);
					context.Users.Add(superAdmin);
					context.SaveChanges();
				}
			}
		}

		return app;
	}
}
