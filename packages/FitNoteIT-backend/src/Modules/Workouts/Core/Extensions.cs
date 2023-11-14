using System.Reflection;
using FitNoteIT.Modules.Workouts.Core.Persistense;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Modules.Workouts.Core;

public static class Extensions
{
	public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddPersistence(configuration);

		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		return services;
	}
}