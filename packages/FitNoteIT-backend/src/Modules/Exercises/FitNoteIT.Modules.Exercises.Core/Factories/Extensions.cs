using FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Exercises.Core.Factories;
internal static class Extensions
{
    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IExerciseFactory, ExerciseFactory>();
        services.AddScoped<IRecordFactory, RecordFactory>();

        return services;
    }
}
