using FitNoteIT.Modules.Users.Core.Abstractions.Factories;
using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Factories;
internal class UserFactory : IUserFactory
{
    public User Create(Guid id, string email, string passwordHash, string userName, DateTime createdAt, Role userRole)
        => new(id, email, passwordHash, userName, createdAt, userRole);
}
