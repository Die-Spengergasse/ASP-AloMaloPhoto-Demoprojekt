using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Interfaces;

public interface IQueryParameter
{
    IPhotoFilterBuilder Compile(string queryParameter, IPhotoFilterBuilder photoFilterBuilder);
}