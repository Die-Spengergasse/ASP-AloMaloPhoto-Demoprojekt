using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public interface IQueryParameter
    {
        IPhotoFilterBuilder Compile(string queryParameter);
    }
}
