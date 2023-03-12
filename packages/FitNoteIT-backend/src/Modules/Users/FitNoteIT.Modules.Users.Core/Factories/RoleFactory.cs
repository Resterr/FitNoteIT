using FitNoteIT.Modules.Users.Core.Abstractions.Factories;
using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Factories;
internal sealed class RoleFactory : IRoleFactory
{
    public Role Create(string name)
        => new(name);

    public Role Create(int id, string name)
    => new(id, name);
}
