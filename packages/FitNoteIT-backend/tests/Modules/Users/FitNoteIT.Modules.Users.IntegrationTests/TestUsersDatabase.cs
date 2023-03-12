using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Shared.Database;
using FitNoteIT.Shared.Tests;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.IntegrationTests;
internal class TestUsersDatabase : IDisposable
{
    public UsersDbContext Context { get; }

    public TestUsersDatabase()
    {
        var options = new OptionsProvider().Get<SqlOptions>("ConnectionStrings");
        Context = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>().UseSqlServer(options.DbConnection).Options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
