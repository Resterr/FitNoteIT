using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Modules.Exercises.Core.Persistence.Contexts;
using FitNoteIT.Modules.Exercises.Core.Persistence.Repositories;
using FitNoteIT.Modules.Exercises.Core.Persistence.Seeders;
using FitNoteIT.Modules.Exercises.Core.Persistence.Services;
using FitNoteIT.Shared.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Exercises.Core.Persistence;
internal static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<ExercisesDbContext>(configuration);
        services.AddScoped<IExercisesSeeder, ExercisesSeeder>();
        services.AddHostedService<ExercisesInitializer>();

        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IRecordRepository, RecordRepository>();

        services.AddScoped<IRecordReadService, RecordReadService>();

        return services;
    }
}
