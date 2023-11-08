using FitNoteIT.Modules.Workouts.API.Requests;
using FitNoteIT.Modules.Workouts.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Workouts.API;
public static class Extensions
{
    public static IServiceCollection AddWorkoutsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreLayer(configuration);

        return services;
    }

    public static WebApplication RegisterWorkoutsModuleRequests(this WebApplication app)
    {
        app.RegisterWorkoutPlansRequests();

        return app;
    }
}