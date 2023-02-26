using FitNoteIT.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Shared.Postgres;
public static class Extensions
{
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<T>(ctx => ctx.UseNpgsql(options.ConnectionString));

        return services;
    }
}
