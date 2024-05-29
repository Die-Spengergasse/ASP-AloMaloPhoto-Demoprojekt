using Spg.AloMalo.Application.Services.PhotoUseCases.Query.Mapper;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Handler
{
    public class PhotoEndsWithHandler
    {
        public IPhotoFilterBuilder Builder { get; }
        public PhotoEndsWithHandler(IPhotoFilterBuilder builder, string queryString)
        {
            string[] parts = queryString.Split(' ');
            if (parts[1] == "ew")
            {
                new PhotoPropertyFilterMapper<string>("Name", builder.ApplyNameEndsWithFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Description", builder.ApplyDescriptionEndsWithFilter, parts[0], parts[2]);
            }
            Builder = builder;
        }
    }
}