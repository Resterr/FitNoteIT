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
		using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
		if (context.Database.GetPendingMigrations()
			.Any())
			return app;
		var usersSeeder = scope.ServiceProvider.GetRequiredService<IUsersSeeder>();

		if (!context.Roles.Any())
		{
			context.Roles.AddRange(usersSeeder.SeedDefaultRoles());
			context.SaveChanges();
		}

		if (context.Users.Include(x => x.Roles)
			.Any(x => x.Roles.Select(y => y.Name)
				.Contains("SuperAdmin")))
			return app;
		{
			var superAdmin = usersSeeder.SeedSuperAdmin();
			var superAdminRole = context.Roles.Single(x => x.Name == "SuperAdmin");
			var adminRole = context.Roles.Single(x => x.Name == "Admin");
			var user = context.Roles.Single(x => x.Name == "User");
					
			superAdmin.AddRole(superAdminRole);
			superAdmin.AddRole(adminRole);
			superAdmin.AddRole(user);
					
			context.Users.Add(superAdmin);
			context.SaveChanges();
		}

		return app;
	}
}
