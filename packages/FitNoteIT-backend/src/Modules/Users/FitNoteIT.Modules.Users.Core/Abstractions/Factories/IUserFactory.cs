using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Abstractions.Factories;
public interface IUserFactory
{
    User Create(Guid id, string email, string passwordHash, string userName, DateTime createdAt, Role userRole);
}