using System.Threading.Channels;
using FitNoteIT.Shared.Events;

namespace FitNoteIT.Shared.Messaging;

internal interface IEventChannel
{
	ChannelReader<IEvent> Reader { get; }
	ChannelWriter<IEvent> Writer { get; }
}