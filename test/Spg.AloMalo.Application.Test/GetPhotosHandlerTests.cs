using Moq;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Queries;
using Spg.AloMalo.Application.Test.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Spg.AloMalo.Repository.Builder;

public class GetPhotosQueryHandlerTests
{
    [Fact]
    public void Handle_ShouldReturnFilteredPhotos_WhenEqFilterIsApplied()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var mockRepository = new Mock<IReadOnlyPhotoRepository>();
        mockRepository.Setup(r => r.GetAllPhotos()).Returns(context.Photos);
        mockRepository.Setup(r => r.FilterBuilder).Returns(new PhotoFilterBuilder(context.Photos));

        var handler = new GetPhotosQueryHandler(mockRepository.Object);
        var query = new GetPhotosQuery(Filter: "name eq Baum", Order: null);
        var queryModel = new GetPhotosQueryModel(query);

        // Act
        var result = handler.Handle(queryModel, CancellationToken.None).Result;

        // Assert
        Assert.Single(result);
        Assert.Equal("Baum", result[0].Name);
    }

    [Fact]
    public void Handle_ShouldReturnFilteredPhotos_WhenStartsWithFilterIsApplied()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var mockRepository = new Mock<IReadOnlyPhotoRepository>();
        mockRepository.Setup(r => r.GetAllPhotos()).Returns(context.Photos);
        mockRepository.Setup(r => r.FilterBuilder).Returns(new PhotoFilterBuilder(context.Photos));

        var handler = new GetPhotosQueryHandler(mockRepository.Object);
        var query = new GetPhotosQuery(Filter: "name bw B", Order: null);
        var queryModel = new GetPhotosQueryModel(query);

        // Act
        var result = handler.Handle(queryModel, CancellationToken.None).Result;

        // Assert
        Assert.Equal(4, result.Count); // Baum, Busch, Berg, Bach
    }

    [Fact]
    public void Handle_ShouldReturnFilteredPhotos_WhenEndsWithFilterIsApplied()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var mockRepository = new Mock<IReadOnlyPhotoRepository>();
        mockRepository.Setup(r => r.GetAllPhotos()).Returns(context.Photos);
        mockRepository.Setup(r => r.FilterBuilder).Returns(new PhotoFilterBuilder(context.Photos));

        var handler = new GetPhotosQueryHandler(mockRepository.Object);
        var query = new GetPhotosQuery(Filter: "name ew h", Order: null);
        var queryModel = new GetPhotosQueryModel(query);

        // Act
        var result = handler.Handle(queryModel, CancellationToken.None).Result;

        // Assert
        Assert.Equal(2, result.Count); // Bach, Busch
    }

    [Fact]
    public void Handle_ShouldReturnFilteredPhotos_WhenContainsFilterIsApplied()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var mockRepository = new Mock<IReadOnlyPhotoRepository>();
        mockRepository.Setup(r => r.GetAllPhotos()).Returns(context.Photos);
        mockRepository.Setup(r => r.FilterBuilder).Returns(new PhotoFilterBuilder(context.Photos));

        var handler = new GetPhotosQueryHandler(mockRepository.Object);
        var query = new GetPhotosQuery(Filter: "name ct a", Order: null);
        var queryModel = new GetPhotosQueryModel(query);

        // Act
        var result = handler.Handle(queryModel, CancellationToken.None).Result;

        // Assert
        Assert.Equal(5, result.Count); // Baum, Bach, Wald, Frau, Mann
    }

    [Fact]
    public void Handle_ShouldReturnFilteredPhotos_WhenMultipleFiltersAreApplied()
    {
        // Arrange
        using var context = DatabaseUtilities.CreateDb();
        var mockRepository = new Mock<IReadOnlyPhotoRepository>();
        mockRepository.Setup(r => r.GetAllPhotos()).Returns(context.Photos);
        mockRepository.Setup(r => r.FilterBuilder).Returns(new PhotoFilterBuilder(context.Photos));

        var handler = new GetPhotosQueryHandler(mockRepository.Object);
        var query = new GetPhotosQuery(Filter: "name ct a name ew h", Order: null);
        var queryModel = new GetPhotosQueryModel(query);

        // Act
        var result = handler.Handle(queryModel, CancellationToken.None).Result;

        // Assert
        Assert.Single(result); // Only "Bach" should match both conditions
        Assert.Equal("Bach", result[0].Name);
    }


}
