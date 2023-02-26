using FitNoteIT.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Shared.Postgres;
public static class Extensions
{
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        var options = configuration.GetOptions<SqlOptions>("SqlServer");
        services.AddDbContext<T>(ctx => ctx.UseSqlServer(options.ConnectionString));

        return services;
    }
}
