using Microsoft.Extensions.DependencyInjection;

namespace FitNoteIT.Shared.Messaging;

internal static class Extensions
{
	public static IServiceCollection AddMessaging(this IServiceCollection services)
	{
		services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
		services.AddTransient<IAsyncEventDispatcher, AsyncEventDispatcher>();
		services.AddSingleton<IEventChannel, EventChannel>();
		services.AddHostedService<EventDispatcherJob>();

		return services;
	}
}