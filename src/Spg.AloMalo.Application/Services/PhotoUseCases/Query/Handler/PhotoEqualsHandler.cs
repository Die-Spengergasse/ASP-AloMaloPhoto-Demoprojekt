using Spg.AloMalo.Application.Services.PhotoUseCases.Query.Mapper;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Handler
{
    public class PhotoEqualsHandler
    {
        public IPhotoFilterBuilder Builder { get; }
        public PhotoEqualsHandler(IPhotoFilterBuilder builder, string queryString)
        {
            string[] parts = queryString.Split(' ');
            if (parts[1] == "eq")
            {
                new PhotoPropertyFilterMapper<string>("Name", builder.ApplyNameEqualsFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Description", builder.ApplyDescriptionEqualsFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Width", builder.ApplyWidthEqualsFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Height", builder.ApplyHeightEqualsFilter, parts[0], parts[2]);
            }
            Builder = builder;
        }
    }
}
