using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Events;
using FitNoteIT.Shared.Queries;

namespace FitNoteIT.Shared.Dispatchers;

public interface IDispatcher
{
	Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
	Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
	Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}