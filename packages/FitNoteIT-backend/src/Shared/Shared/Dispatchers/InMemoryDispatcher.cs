using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Events;
using FitNoteIT.Shared.Queries;

namespace FitNoteIT.Shared.Dispatchers;

internal sealed class InMemoryDispatcher : IDispatcher
{
	private readonly ICommandDispatcher _commandDispatcher;
	private readonly IEventDispatcher _eventDispatcher;
	private readonly IQueryDispatcher _queryDispatcher;

	public InMemoryDispatcher(
		ICommandDispatcher commandDispatcher,
		IEventDispatcher eventDispatcher,
		IQueryDispatcher queryDispatcher)
	{
		_commandDispatcher = commandDispatcher;
		_eventDispatcher = eventDispatcher;
		_queryDispatcher = queryDispatcher;
	}

	public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
	{
		return _commandDispatcher.SendAsync(command, cancellationToken);
	}

	public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
	{
		return _eventDispatcher.PublishAsync(@event, cancellationToken);
	}

	public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
	{
		return _queryDispatcher.QueryAsync(query, cancellationToken);
	}
}