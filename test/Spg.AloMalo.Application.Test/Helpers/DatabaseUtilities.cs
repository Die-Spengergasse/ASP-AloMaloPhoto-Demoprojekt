using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Test.Helpers;


namespace Spg.AloMalo.Application.Test.Helpers
{
    public static class DatabaseUtilities
    {
        public static PhotoContext CreateDb()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<PhotoContext>()
                .UseSqlite(connection)
                .Options;

            var db = new PhotoContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var photographers = GetSeedingPhotographers();
            var albums = GetSeedingAlbums(photographers);
            var photos = GetSeedingPhotos(photographers);
            var albumPhotos = GetSeedingAlbumPhotos(albums, photos);

            db.Photographers.AddRange(photographers);
            db.Albums.AddRange(albums);
            db.Photos.AddRange(photos);
            db.AlbumPhotos.AddRange(albumPhotos);

            db.SaveChanges();

            return db;
        }

        public static List<Photographer> GetSeedingPhotographers()
        {
            return new List<Photographer>
            {
                new Photographer(
                    new Guid("99999999-9999-9999-9999-999999999999"),
                    "Martin",
                    "Schrutek",
                    new Address("Photo Street 1", "1234", "Photoville", "Photanien"){ State = new State { Name = "NÖ" } },
                    new PhoneNumber(43, 2222, "258963147"),
                    new PhoneNumber(43, 1234, "123456789"),
                    new List<EMail> { new EMail("schrutek@spengergasse.at"), new EMail("schrutek2@spengergasse.at") },
                    new EMail("schrutek@spengergasse.at")
                ),
                new Photographer(
                    new Guid("88888888-8888-8888-8888-888888888888"),
                    "Klaus",
                    "Unger",
                    new Address("Photo Street 2", "7985", "Photoville 2", "Photanien 2"){ State = new State { Name = "WIEN" } },
                    new PhoneNumber(43, 4561, "987654123"),
                    new PhoneNumber(43, 1326, "654789321"),
                    new List<EMail> { new EMail("unger@spengergasse.at"), new EMail("unger3@spengergasse.at") },
                    new EMail("unger@spengergasse.at")
                )
            };
        }

        public static List<Album> GetSeedingAlbums(List<Photographer> photographers)
        {
            return new List<Album>
            {
                new Album("Landschaften", "Beschreibung 1", false, photographers[0], new TimeStampProvider()),
                new Album("Menschen", "Beschreibung 2", false, photographers[0], new TimeStampProvider()),
                new Album("Dinge", "Beschreibung 3", false, photographers[1], new TimeStampProvider())
            };
        }

        public static List<Photo> GetSeedingPhotos(List<Photographer> photographers)
        {
            return new List<Photo>
            {
                new Photo(new Guid("11111111-1111-1111-1111-111111111111"), "Baum", "Beschreibung Baum", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 100, false, photographers[0]),
                new Photo(new Guid("22222222-2222-2222-2222-222222222222"), "Busch", "Beschreibung Busch", DateTime.Now, ImageTypes.Png, new Location(123, 456), 300, 100, false, photographers[0]),
                new Photo(new Guid("33333333-3333-3333-3333-333333333333"), "Berg", "Beschreibung Berg", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 800, false, photographers[1]),
                new Photo(new Guid("44444444-4444-4444-4444-444444444444"), "Bach", "Beschreibung Bach", DateTime.Now, ImageTypes.Png, new Location(123, 456), 400, 500, false, photographers[1]),
                new Photo(new Guid("55555555-5555-5555-5555-555555555555"), "Wald", "Beschreibung Wald", DateTime.Now, ImageTypes.Png, new Location(123, 456), 800, 400, false, photographers[1]),
                new Photo(new Guid("66666666-6666-6666-6666-666666666666"), "Frau", "Beschreibung Frau", DateTime.Now, ImageTypes.Png, new Location(123, 456), 800, 400, false, photographers[0]),
                new Photo(new Guid("77777777-7777-7777-7777-777777777777"), "Mann", "Beschreibung Mann", DateTime.Now, ImageTypes.Png, new Location(123, 456), 1000, 600, false, photographers[0]),
                new Photo(new Guid("88888888-8888-8888-8888-888888888888"), "Wiese", "Beschreibung Wiese", DateTime.Now, ImageTypes.Png, new Location(123, 456), 200, 1000, false, photographers[1])
            };
        }

        public static List<AlbumPhoto> GetSeedingAlbumPhotos(List<Album> albums, List<Photo> photos)
        {
            return new List<AlbumPhoto>
            {
                new AlbumPhoto(albums[0], photos[0], 1),
                new AlbumPhoto(albums[0], photos[1], 2),
                new AlbumPhoto(albums[0], photos[2], 3),
                new AlbumPhoto(albums[0], photos[3], 4),
                new AlbumPhoto(albums[0], photos[4], 5),
                new AlbumPhoto(albums[1], photos[5], 1),
                new AlbumPhoto(albums[1], photos[6], 2),
                new AlbumPhoto(albums[1], photos[7], 3)
            };
        }
    }
}
