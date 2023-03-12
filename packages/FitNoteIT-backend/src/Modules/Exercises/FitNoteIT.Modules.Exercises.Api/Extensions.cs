using FitNoteIT.Modules.Exercises.Api.Requests;
using FitNoteIT.Modules.Exercises.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Exercises.Api;
public static class Extensions
{
    public static IServiceCollection AddExercisesModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreLayer(configuration);

        return services;
    }

    public static WebApplication RegisterExercisesModuleRequests(this WebApplication app)
    {
        app.RegisterExercisesRequests();
        app.RegisterRecordsRequests();

        return app;
    }
}