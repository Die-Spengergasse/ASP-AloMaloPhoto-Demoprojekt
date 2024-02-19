using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Application.Test.Helpers 
{ 
    public static class DatabaseUtilities 
    {
        public static PhotoContext CreateDb()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            PhotoContext db = new PhotoContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
    }
} 
