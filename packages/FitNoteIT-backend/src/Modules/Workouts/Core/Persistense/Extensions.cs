using FitNoteIT.Modules.Workouts.Core.Persistense.Clients;
using FitNoteIT.Modules.Workouts.Core.Persistense.Options;
using FitNoteIT.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Security.Authentication;

namespace FitNoteIT.Modules.Workouts.Core.Persistense;
internal static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<WorkoutsOptions>("ConnectionStrings");

        services.AddSingleton(x => {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(options.MongoDb));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            return new WorkoutsMongoClient(settings);
        });
        
        return services;
    }
}
