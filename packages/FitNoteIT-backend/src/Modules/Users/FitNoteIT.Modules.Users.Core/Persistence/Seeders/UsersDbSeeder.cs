using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Modules.Users.Core.Security;
using FitNoteIT.Shared.Options;
using FitNoteIT.Shared.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace FitNoteIT.Modules.Users.Core.Persistence.Seeders;
internal class UsersDbSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public UsersDbSeeder(IServiceProvider serviceProvider, IConfiguration configuration, IPasswordManager passwordManager, IClock clock)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Roles.AnyAsync(cancellationToken) == false)
        {
            var roles = new List<Role>
            {
                new Role("SuperAdmin"),
                new Role("Admin"),
                new Role("User"),
            };

            await dbContext.Roles.AddRangeAsync(roles, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken); ;
        }

        if (await dbContext.Users.AnyAsync(cancellationToken) == false)
        {
            var superAdminConfig = _configuration.GetOptions<SuperAdminOptions>("SuperAdminAccount");

            var superAdminRole = await dbContext.Roles.SingleAsync(x => x.Name == "SuperAdmin", cancellationToken);
            var securedPassword = _passwordManager.Secure(superAdminConfig.Password);

            var superAdmin = new User(Guid.NewGuid(), superAdminConfig.Email, securedPassword, superAdminConfig.UserName, _clock.CurrentDate(), superAdminRole);

            await dbContext.Users.AddAsync(superAdmin, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken); ;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
