using FitNoteIT.Modules.Users.Core;
using FitNoteIT.Modules.Users.Api.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Users.Api;
public static class Extensions
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreLayer(configuration);

        return services;
    }

    public static WebApplication RegisterUsersModuleRequests(this WebApplication app)
    {
        app.RegisterUsersRequests();

        return app;
    }
}
