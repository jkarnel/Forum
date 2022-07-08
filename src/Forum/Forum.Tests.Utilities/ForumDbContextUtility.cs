using Forum.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Forum.Tests.Utilities
{
    public class ForumDbContextUtility
    {
        public static ForumDbContext GetInMemoryDBContext()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ForumDbContext(options);
            dbContext.Database.EnsureCreated();
            
            return dbContext;
        }
    }
}
