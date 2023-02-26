using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Modules.Users.Core.Persistence.Repositories;
using FitNoteIT.Modules.Users.Core.Persistence.Seeders;
using FitNoteIT.Modules.Users.Core.Persistence.Services;
using FitNoteIT.Modules.Users.Core.Repositories;
using FitNoteIT.Modules.Users.Core.Services;
using FitNoteIT.Shared.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Users.Core.Persistence;
internal static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<UsersDbContext>(configuration);
        services.AddHostedService<UsersDbSeeder>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserReadService, UserReadService>();
        services.AddScoped<IRoleReadService, RoleReadService>();

        return services;
    }
}
