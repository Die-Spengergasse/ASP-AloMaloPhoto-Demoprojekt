using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Mapper
{
    public class PhotoPropertyFilterMapper<T>
    {
        public PhotoPropertyFilterMapper(string propertyNameToBeMapped, Func<T,IPhotoFilterBuilder> function, string actualPropertyName, T actualPropertyValue)
        {
            if(propertyNameToBeMapped == actualPropertyName)
            {
                function.Invoke(actualPropertyValue);
            }
        }
    }
}