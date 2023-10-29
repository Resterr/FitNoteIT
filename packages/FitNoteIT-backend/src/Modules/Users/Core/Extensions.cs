using System.Reflection;
using FitNoteIT.Modules.Users.Core.Persistence;
using FitNoteIT.Modules.Users.Core.Security;
using FitNoteIT.Modules.Users.Core.Services;
using FitNoteIT.Modules.Users.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Users.Core;

public static class Extensions
{
	public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddPersistence();
		services.AddSecurity(configuration);
		services.AddTransient<IUsersModuleApi, UsersModuleApi>();

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