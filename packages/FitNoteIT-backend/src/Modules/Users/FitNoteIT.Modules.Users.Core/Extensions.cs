using FitNoteIT.Modules.Users.Core.Factories;
using FitNoteIT.Modules.Users.Core.Persistence;
using FitNoteIT.Modules.Users.Core.Security;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FitNoteIT.Modules.Users.Core;
public static class Extensions
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddSecurity();
        services.AddFactories();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
