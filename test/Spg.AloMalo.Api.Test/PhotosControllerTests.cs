using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Spg.AloMalo.Api.Controllers;
using Spg.AloMalo.Application.Mock;
using Spg.AloMalo.Application.Services;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.MockMvc;
using Spg.AloMalo.Repository;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Api.Test
{
    public class PhotosControllerTests
    {
        public PhotoContext CreateDb()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            PhotoContext db = new PhotoContext(options);
            db.Database.EnsureCreated();
            return db;
        }

        //[Fact]
        //public void POST_ShouldCreatePhoto_WhenCommandIsOk()
        //{
        //    PhotoContext db = CreateDb();

        //    PhotosController utt = new PhotosController(
        //        new PhotoService(null,
        //            new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)),
        //            new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)),
        //            null,
        //                new DateTimeServiceMock()));
            
        //    IActionResult result = utt.GetPhoto();
        //    Assert.Equal(result, new OkObjectResult(null));
        //}
    }
}