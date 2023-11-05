using FitNoteIT.Shared.Database.Interceptors;
using FitNoteIT.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Shared.Database;

public static class Extensions
{
	private const string _sectionName = "ConnectionStrings";

	internal static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<ConnectionStringsOptions>(configuration.GetSection(_sectionName));
		services.AddHostedService<DbContextAppInitializer>();

		return services;
	}

	public static IServiceCollection AddSqlServer<T>(this IServiceCollection services) where T : DbContext
	{
		var configuration = services.BuildServiceProvider()
			.GetRequiredService<IConfiguration>();
		var connectionString = configuration.GetOptions<ConnectionStringsOptions>(_sectionName)
			.SqlServer;
		services.AddDbContext<T>(x => x.UseSqlServer(connectionString));

		services.AddScoped<AuditableEntitySaveChangesInterceptor>();

		return services;
	}
}