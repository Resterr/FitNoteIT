using FitNoteIT.Modules.Users.API.Requests;
using FitNoteIT.Modules.Users.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Users.API;

public static class Extensions
{
	public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddCoreLayer(configuration);

		return services;
	}

	public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
	{
		app.UseCoreLayer();

		return app;
	}

	public static WebApplication RegisterUsersModuleRequests(this WebApplication app)
	{
		app.RegisterUsersRequests();
		app.RegisterAdminRequests();

		return app;
	}
}