using FitNoteIT.Modules.Users.Core.Abstractions.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Users.Core.Factories;
internal static class Extensions
{
    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IUserFactory, UserFactory>();

        return services;
    }
}
