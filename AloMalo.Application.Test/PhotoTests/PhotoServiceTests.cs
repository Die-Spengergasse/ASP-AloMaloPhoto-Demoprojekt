using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;
using Spg.AloMalo.Repository.Test.Helpers;

namespace AloMalo.Application.Test.PhotoServiceTests
{
    public class PhotoServiceTests
    {
        public void OK_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestIfInsertWorks()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                Assert.Equal(8, db.Photos.Count());
            }
        }

        [Fact]
        public void Should_Get2Photos_When_NameStartsWithW()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new Spg.AloMalo.DomainModel.Queries.GetPhotosQuery("name sw w"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public void Should_Get3Photos_When_NameContainsU()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new Spg.AloMalo.DomainModel.Queries.GetPhotosQuery("name ct u"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void Should_Get3Photos_When_HeightGreaterThan500()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new Spg.AloMalo.DomainModel.Queries.GetPhotosQuery("height gt 500"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void Should_Get4Photos_When_HeightGreaterThanEqual500()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new Spg.AloMalo.DomainModel.Queries.GetPhotosQuery("height gte 500"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(4, result.Count());
            }
        }

        [Fact]
        public void Should_Get4Photos_When_HeightLowerThan500()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new Spg.AloMalo.DomainModel.Queries.GetPhotosQuery("height lt 500"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(4, result.Count());
            }
        }

        [Fact]
        public void Should_Get5Photos_When_HeightLowerThanEqual500()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new Spg.AloMalo.DomainModel.Queries.GetPhotosQuery("height lte 500"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(5, result.Count());
            }
        }
    }
}
