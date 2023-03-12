using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Users.Core.Abstractions.Services;
using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Modules.Users.Core.Persistence.Repositories;
using FitNoteIT.Modules.Users.Core.Persistence.Seeders;
using FitNoteIT.Modules.Users.Core.Persistence.Services;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Shared.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Users.Core.Persistence;
internal static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<UsersDbContext>(configuration);
        services.AddScoped<IUsersSeeder, UsersSeeder>();
        services.AddHostedService<UsersInitializer>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleReadService, RoleReadService>();

        services.AddScoped<IUsersModuleApi, UsersModuleApi>();

        return services;
    }
}
