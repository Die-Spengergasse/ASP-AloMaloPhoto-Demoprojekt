using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Api.Test.Helpers 
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
                    new Guid("99999999-9999-9999-9999-999999999999"),
                    "Martin",
                    "Schrutek",
                    new Address("Photo Street 1", "1234", "Photoville", "Photanien"){ State= new() { Name = "NÖ" } }, //, new State("NÖ")
                    new PhoneNumber(43, 2222, "258963147"),
                    new PhoneNumber(43, 1234, "123456789"),
                    new List<EMail>() { new EMail("schrutek@spengergasse.at"), new EMail("schrutek2@spengergasse.at") },
                    new EMail("schrutek@spengergasse.at")
                ),
                new Photographer(
                    new Guid("99999999-9999-9999-9999-999999999999"),
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

        public static List<Album> GetSeedingAlbums(List<Photographer> photographers)
        {
            return new List<Album>()
            {
                new Album("Landschaften", "ewrtzujkjgfzhtdrs asdfgh cd", false, photographers.ElementAt(0), new TimeStampProvider()),
                new Album("Menschen", "daefsrgdtfhjk sdfrgth kjh sd d", false, photographers.ElementAt(0), new TimeStampProvider()),
                new Album("Dinge", "defsrgthzujki jkjhg mjnhbgv ffsdas", false, photographers.ElementAt(1), new TimeStampProvider()),
            };
        }

        public static List<Photo> GetSeedingPhotos(List<Photographer> photographers)
        {
            return new List<Photo>()
            {
                new Photo(new Guid("11111111-1111-1111-1111-111111111111"), "Baum", "addaas fghjkl adefg", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 100, false, photographers.ElementAt(0)),
                new Photo(new Guid("22222222-2222-2222-2222-222222222222"), "Busch", "qweewqeqw dfrgv dsd", DateTime.Now, ImageTypes.Png, new Location(123, 456), 300, 100, false, photographers.ElementAt(0)),
                new Photo(new Guid("33333333-3333-3333-3333-333333333333"), "Berg", "fgdgfdgfd tghzjkjhgf", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 800, false, photographers.ElementAt(1)),
                new Photo(new Guid("44444444-4444-4444-4444-444444444444"), "Bach", "fhjdscsdfghjnhbgvfd", DateTime.Now, ImageTypes.Png, new Location(123, 456), 400, 500, false, photographers.ElementAt(1)),
                new Photo(new Guid("55555555-5555-5555-5555-555555555555"), "Wald", "ertzhjkl,mnbvcd sedgtf", DateTime.Now, ImageTypes.Png, new Location(123, 456), 800, 400, false, photographers.ElementAt(1)),
                new Photo(new Guid("66666666-6666-6666-6666-666666666666"), "Frau", "dfvbnjijuhfds dsfdgh", DateTime.Now, ImageTypes.Png, new Location(123, 456), 800, 400, false, photographers.ElementAt(0)),
                new Photo(new Guid("77777777-7777-7777-7777-777777777777"), "Mann", "wsedrtzujikkjhgvfc ed", DateTime.Now, ImageTypes.Png, new Location(123, 456), 1000, 600, false, photographers.ElementAt(0)),
                new Photo(new Guid("88888888-8888-8888-8888-888888888888"), "Wiese", "oiujztrdefvgh hjuhki", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 1000, false, photographers.ElementAt(1))
            };
        }

        public static List<AlbumPhoto> GetSeedingAlbumPhotos(List<Album> albums, List<Photo> photos)
        {
            return new List<AlbumPhoto>()
            {
                new AlbumPhoto(albums.ElementAt(0), photos.ElementAt(0), 1),
                new AlbumPhoto(albums.ElementAt(0), photos.ElementAt(1), 2),
                new AlbumPhoto(albums.ElementAt(0), photos.ElementAt(2), 3),
                new AlbumPhoto(albums.ElementAt(0), photos.ElementAt(3), 4),
                new AlbumPhoto(albums.ElementAt(0), photos.ElementAt(4), 5),
                new AlbumPhoto(albums.ElementAt(1), photos.ElementAt(5), 1),
                new AlbumPhoto(albums.ElementAt(1), photos.ElementAt(6), 2),
                new AlbumPhoto(albums.ElementAt(1), photos.ElementAt(7), 3),
            };
        }
    }
} 
