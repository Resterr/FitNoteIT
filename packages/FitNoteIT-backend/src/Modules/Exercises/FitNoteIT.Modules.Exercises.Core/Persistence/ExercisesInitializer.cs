using FitNoteIT.Modules.Exercises.Core.Persistence.Contexts;
using FitNoteIT.Modules.Exercises.Core.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitNoteIT.Modules.Exercises.Core.Persistence;
internal class ExercisesInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public ExercisesInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ExercisesDbContext>();

        if (dbContext.Database.IsRelational())
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        var exerciseSeeder = scope.ServiceProvider.GetRequiredService<IExercisesSeeder>();

        if (await dbContext.Exercises.AnyAsync(cancellationToken) == false)
        {
            var exercises = exerciseSeeder.SeedExercises();

            await dbContext.Exercises.AddRangeAsync(exercises, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken); ;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
