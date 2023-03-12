using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Abstractions.Factories;
internal interface IRoleFactory
{
    Role Create(int id, string name);
    Role Create(string name);
}
