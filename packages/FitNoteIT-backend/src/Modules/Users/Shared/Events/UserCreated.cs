using FitNoteIT.Shared.Events;

namespace FitNoteIT.Modules.Users.Shared.Events;

public record UserCreated(Guid Id, string Email, string UserName) : IEvent;