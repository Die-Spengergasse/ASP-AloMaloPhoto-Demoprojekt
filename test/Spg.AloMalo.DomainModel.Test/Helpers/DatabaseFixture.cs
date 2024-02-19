using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Test.Helpers
{
    public class DatabaseFixture// : IDisposable
    {
        //public DatabaseFixture()
        //{
        //    DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
        //    dbContextOptionsBuilder.UseSqlite("Data Source=.\\..\\..\\..\\..\\..\\Photo.db");

        //    Db = new PhotoContext(dbContextOptionsBuilder.Options);
        //    Db.Database.EnsureDeleted();
        //    Db.Database.EnsureCreated();
        //}
        public DatabaseFixture()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            Db = new PhotoContext(options);
            Db.Database.EnsureCreated();
        }

        //public void Dispose()
        //{
        //    Db.Dispose();
        //}

        public PhotoContext Db { get; private set; }
    }
}
