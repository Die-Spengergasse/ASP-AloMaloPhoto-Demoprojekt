using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface ICreateablePhotoRepository
    {
        void Create(Photo photo);
    }

    public interface IDeleteablePhotoRepository
    {
        void Delete(PhotoId id);
    }

    public interface IUpdateablePhotoRepository
    {
        IUpdateBuilderBase<Photo, IPhotoUpdateBuilder> UpdateBuilder { get; }
    }

    public interface IWritablePhotoRepository: IUpdateablePhotoRepository, ICreateablePhotoRepository, IDeleteablePhotoRepository
    {
        //IUpdateBuilderBase<Photo, IPhotoUpdateBuilder> UpdateBuilder { get; }
    }
}
