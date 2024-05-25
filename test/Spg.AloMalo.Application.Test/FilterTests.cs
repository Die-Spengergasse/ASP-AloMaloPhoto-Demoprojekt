//using Spg.AloMalo.DomainModel.Model;
//using Spg.AloMalo.Application.Test.Helpers;
//using System.Linq;
//using Xunit;
//using Spg.AloMalo.Application.Services.PhotoUseCases.Query;

//public class FilterTests
//{
//    [Fact]
//    public void EqualsFilter_ShouldReturnCorrectResultsForPhotos()
//    {
//        using var context = DatabaseUtilities.CreateDb();
//        var photos = context.Photos.ToList();

//        var filter = new EqualsFilter<Photo>("Name", "Baum");
//        var result = photos.Where(p => filter.Apply(p)).ToList();

//        Assert.Single(result);
//        Assert.Equal("Baum", result[0].Name);
//    }

//    [Fact]
//    public void EqualsFilter_ShouldReturnCorrectResultsForPhotosHeight()
//    {
//        using var context = DatabaseUtilities.CreateDb();
//        var photos = context.Photos.ToList();

//        var filter = new EqualsFilter<Photo>("Height", 100);
//        var result = photos.Where(p => filter.Apply(p)).ToList();

        
//        Assert.Equal(2, result.Count);
//    }

//    [Fact]
//    public void StartsWithFilter_ShouldReturnCorrectResultsForPhotos()
//    {
//        using var context = DatabaseUtilities.CreateDb();
//        var photos = context.Photos.ToList();

//        var filter = new StartsWithFilter<Photo>("Name", "B");
//        var result = photos.Where(p => filter.Apply(p)).ToList();

//        Assert.Equal(4, result.Count); // Baum, Busch, Berg, Bach, Busch
//    }

//    [Fact]
//    public void EndsWithFilter_ShouldReturnCorrectResultsForPhotos()
//    {
//        using var context = DatabaseUtilities.CreateDb();
//        var photos = context.Photos.ToList();

//        var filter = new EndsWithFilter<Photo>("Name", "ch");
//        var result = photos.Where(p => filter.Apply(p)).ToList();

//        Assert.Equal(2, result.Count);
//    }

//    [Fact]
//    public void ContainsFilter_ShouldReturnCorrectResultsForPhotos()
//    {
//        using var context = DatabaseUtilities.CreateDb();
//        var photos = context.Photos.ToList();

//        var filter = new ContainsFilter<Photo>("Name", "au");
//        var result = photos.Where(p => filter.Apply(p)).ToList();

//        Assert.Equal(2, result.Count);
//    }

//    [Fact]
//    public void ContainsFilter_ShouldReturnCorrectResultsForPhotographers()
//    {
//        using var context = DatabaseUtilities.CreateDb();
//        var photographers = context.Photographers.ToList();

//        var filter = new ContainsFilter<Photographer>("FirstName", "art");
//        var result = photographers.Where(p => filter.Apply(p)).ToList();

//        Assert.Equal("Martin", result[0].FirstName);
//    }
//}
