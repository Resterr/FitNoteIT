using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Modules.Users.Core.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitNoteIT.Modules.Users.Core.Persistence;
internal class UsersInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public UsersInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
        var userSeeder = scope.ServiceProvider.GetRequiredService<IUsersSeeder>();

        if (await dbContext.Roles.AnyAsync(cancellationToken) == false)
        {
            var roles = userSeeder.SeedRoles();

            await dbContext.Roles.AddRangeAsync(roles, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken); ;
        }

        if (await dbContext.Users.AnyAsync(cancellationToken) == false)
        {
            var superAdminRole = await dbContext.Roles.SingleAsync(x => x.Name == "SuperAdmin", cancellationToken);

            var superAdmin = userSeeder.SeedSuperAdmin(superAdminRole);

            await dbContext.Users.AddAsync(superAdmin, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken); ;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
