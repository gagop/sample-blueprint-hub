using Gakko.API.Context;
using Microsoft.EntityFrameworkCore;

namespace Gakko.Tests.Setup;

public class GakkoDbContextForTestsFactory
{
    public static GakkoContext CreateDbContextForInMemory()
    {
        var options = new DbContextOptionsBuilder<GakkoContext>()
            .UseInMemoryDatabase("TestDatabase" + Guid.NewGuid())
            .Options;
        var dbContext = new GakkoContext(options);

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}