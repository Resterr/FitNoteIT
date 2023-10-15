using FitNoteIT.Shared.Events;

namespace FitNoteIT.Shared.Messaging;

public interface IMessageBroker
{
	Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}