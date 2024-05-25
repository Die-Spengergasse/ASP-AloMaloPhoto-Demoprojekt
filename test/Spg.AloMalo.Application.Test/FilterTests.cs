using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Application.Test.Utilities;
using System.Linq;
using Xunit;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

public class FilterTests
{
    [Fact]
    public void EqualsFilter_ShouldReturnMatchingPhotos()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var photos = context.Photos.ToList();
        var filter = new EqualsFilter<Photo>("Name", "Baum");

        // Act
        var result = photos.Where(p => filter.Apply(p)).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Baum", result[0].Name);
    }

    [Fact]
    public void StartsWithFilter_ShouldReturnMatchingPhotos()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var photos = context.Photos.ToList();
        var filter = new StartsWithFilter<Photo>("Name", "B");

        // Act
        var result = photos.Where(p => filter.Apply(p)).ToList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Contains(result, p => p.Name == "Baum");
        Assert.Contains(result, p => p.Name == "Busch");
        Assert.Contains(result, p => p.Name == "Berg");
        Assert.Contains(result, p => p.Name == "Bach");
    }

    [Fact]
    public void EndsWithFilter_ShouldReturnMatchingPhotos()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var photos = context.Photos.ToList();
        var filter = new EndsWithFilter<Photo>("Name", "h");

        // Act
        var result = photos.Where(p => filter.Apply(p)).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Name == "Bach");
        Assert.Contains(result, p => p.Name == "Busch");
    }

    [Fact]
    public void ContainsFilter_ShouldReturnMatchingPhotos()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var photos = context.Photos.ToList();
        var filter = new ContainsFilter<Photo>("Name", "a");

        // Act
        var result = photos.Where(p => filter.Apply(p)).ToList();

        // Assert
        Assert.Equal(5, result.Count);
        Assert.Contains(result, p => p.Name == "Baum");
        Assert.Contains(result, p => p.Name == "Bach");
        Assert.Contains(result, p => p.Name == "Wald");
        Assert.Contains(result, p => p.Name == "Frau");
        Assert.Contains(result, p => p.Name == "Mann");

    }
}
