namespace Spg.AloMalo.Api.Test
{
    public class PhotosControllerTests
    {
        [Fact]
        public void OK_Test()
        {
            Assert.True(true);
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