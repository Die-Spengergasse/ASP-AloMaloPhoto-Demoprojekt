using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoUpdateBuilder : IEntityUpdateBuilder<Photo>
    {
        IPhotoUpdateBuilder WithName(string name);
        IPhotoUpdateBuilder WithOrientation(Orientations orientation);
    }
}
