using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.DomainModel.Test.Helpers 
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
                new Photo("Baum", "addaas fghjkl adefg", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 100, Orientations.Landscape, false, photographers.ElementAt(0)),
                new Photo("Busch", "qweewqeqw dfrgv dsd", DateTime.Now, ImageTypes.Png, new Location(123, 456), 300, 100, Orientations.Landscape, false, photographers.ElementAt(0)),
                new Photo("Berg", "fgdgfdgfd tghzjkjhgf", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 800, Orientations.Portrait, false, photographers.ElementAt(1)),
                new Photo("Bach", "fhjdscsdfghjnhbgvfd", DateTime.Now, ImageTypes.Png, new Location(123, 456), 400, 500, Orientations.Portrait, false, photographers.ElementAt(1)),
                new Photo("Wald", "ertzhjkl,mnbvcd sedgtf", DateTime.Now, ImageTypes.Png, new Location(123, 456), 800, 400, Orientations.Landscape, false, photographers.ElementAt(1)),
                new Photo("Frau", "dfvbnjijuhfds dsfdgh", DateTime.Now, ImageTypes.Png, new Location(123, 456), 800, 400, Orientations.Landscape, false, photographers.ElementAt(0)),
                new Photo("Mann", "wsedrtzujikkjhgvfc ed", DateTime.Now, ImageTypes.Png, new Location(123, 456), 1000, 600, Orientations.Landscape, false, photographers.ElementAt(0)),
                new Photo("Wiese", "oiujztrdefvgh hjuhki", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 1000, Orientations.Portrait, false, photographers.ElementAt(1))
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
