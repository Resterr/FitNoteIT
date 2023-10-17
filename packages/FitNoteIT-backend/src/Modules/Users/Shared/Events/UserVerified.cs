using FitNoteIT.Shared.Events;

namespace FitNoteIT.Modules.Users.Shared.Events;

public record UserVerified(Guid UserId, string Email, string Nationality) : IEvent;