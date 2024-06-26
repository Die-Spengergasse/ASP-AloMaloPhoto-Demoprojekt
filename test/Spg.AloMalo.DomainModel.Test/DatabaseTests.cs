﻿using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Model.RichTypes;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Test
{
    public class DatabaseTests
    {
        private PhotoContext CreateDb()
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseSqlite("Data Source=.\\..\\..\\..\\..\\..\\Photo.db");

            PhotoContext db= new PhotoContext(dbContextOptionsBuilder.Options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            return db;
        }

        private void SeedDb(PhotoContext db)
        {
            Photographer newPhotographer = new Photographer(
                new Guid("99999999-9999-9999-9999-999999999999"),
                "Martin",
                "Schrutek",
                new Address("Photo Street 1", "1234", "Photoville", "Photanien") { State = new() { Name = "NÖ" } }, //, new State("NÖ")
                new PhoneNumber(43, 1234, "123456789"),
                new PhoneNumber(43, 1234, "123456789"),
                new List<EMail>() { new EMail("schrutek@spengergasse.at"), new EMail("schrutek2@spengergasse.at") },
                new EMail("schrutek@spengergasse.at")
            );
            Album newAlbum = new Album(
                "Test Album 01", "Beschreibung...",
                true,
                newPhotographer,
                new TimeStampProvider()
            );
            Photo newPhoto1 = new Photo(
                new Guid("11111111-1111-1111-1111-111111111111"),
                "Test Photo bqwe",
                "Beschreibung Test Photo 01...",
                DateTime.Now,
                ImageTypes.Png,
                new Location(12, 17),
                400, 800,
                false,
                newPhotographer
            );
            Photo newPhoto2 = new Photo(
                new Guid("11111111-1111-1111-1111-111111111111"),
                "My Photo aqweqe",
                "Beschreibung Test Photo 01...",
                DateTime.Now,
                ImageTypes.Png,
                new Location(12, 17),
                400, 800,
                false,
                newPhotographer
            );
            Photo newPhoto3 = new Photo(
                new Guid("11111111-1111-1111-1111-111111111111"),
                "Test Photo arererttre",
                "Beschreibung Test Photo 01...",
                DateTime.Now,
                ImageTypes.Png,
                new Location(12, 17),
                400, 800,
                false,
                newPhotographer
            );
            Photo newPhoto4 = new Photo(
                new Guid("11111111-1111-1111-1111-111111111111"),
                "My Photo axvvxxvcxvc",
                "Beschreibung Test Photo 01...",
                DateTime.Now,
                ImageTypes.Png,
                new Location(12, 17),
                400, 800,
                false,
                newPhotographer
            );
            Photo newPhoto5 = new Photo(
                new Guid("11111111-1111-1111-1111-111111111111"),
                "Test Photo xdfssfdsfdfd",
                "Beschreibung Test Photo 01...",
                DateTime.Now,
                ImageTypes.Png,
                new Location(12, 17),
                400, 800,
                false,
                newPhotographer
            );

            // Act
            db.Photographers.Add(newPhotographer);
            db.Albums.Add(newAlbum);
            db.Photos.Add(newPhoto1);
            db.Photos.Add(newPhoto2);
            db.Photos.Add(newPhoto3);
            db.Photos.Add(newPhoto4);
            db.Photos.Add(newPhoto5);
            db.SaveChanges();
        }

        [Fact()]
        public void Db_Should_Create()
        {
            using (PhotoContext db = CreateDb())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                SeedDb(db);
            }
        }
    }
}
