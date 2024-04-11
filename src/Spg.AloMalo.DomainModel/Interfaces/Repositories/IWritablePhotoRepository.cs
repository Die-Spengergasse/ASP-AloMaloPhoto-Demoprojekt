using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Validators.RichTypes;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface ICreateablePhotoRepository
    {
        void Create(Photo photo);
    }

    public interface IDeleteablePhotoRepository
    {
        void Delete<TId>(IRichType<TId> id);
    }

    public interface IUpdateablePhotoRepository
    {
        IUpdateBuilderBase<Photo, IPhotoUpdateBuilder> UpdateBuilder { get; }
    }

    public interface IWritablePhotoRepository : 
        IUpdateablePhotoRepository, ICreateablePhotoRepository, IDeleteablePhotoRepository
    { }
}
