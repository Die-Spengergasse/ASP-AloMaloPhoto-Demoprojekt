using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Repository.Test.Helpers 
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

        public static List<Photographer> GetSeedingPhotographers()
        {
            return new List<Photographer>()
            {
                new Photographer(
                    "Martin",
                    "Schrutek",
                    new Address("Photo Street 1", "1234", "Photoville", "Photanien"){ State= new() { Name = "NÖ" } }, //, new State("NÖ")
                    new PhoneNumber(43, 2222, "258963147"),
                    new PhoneNumber(43, 1234, "123456789"),
                    new List<EMail>() { new EMail("schrutek@spengergasse.at"), new EMail("schrutek2@spengergasse.at") },
                    new EMail("schrutek@spengergasse.at")
                ),
                new Photographer(
                    "Klaus",
                    "Unger",
                    new Address("Photo Street 2", "7985", "Photoville 2", "Photanien 2"){ State= new() { Name = "WIEN" } }, //, new State("NÖ")
                    new PhoneNumber(43, 4561, "987654123"),
                    new PhoneNumber(43, 1326, "654789321"),
                    new List<EMail>() { new EMail("schrutek@spengergasse.at"), new EMail("unger3@spengergasse.at") },
                    new EMail("unger@spengergasse.at")
                )
            };
        }
    }
}
