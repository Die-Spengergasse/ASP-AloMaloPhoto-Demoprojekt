using Spg.AloMalo.Application.Services.PhotoUseCases.Query.Mapper;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Handler
{
    public class PhotoContainsHandler
    {
        public IPhotoFilterBuilder Builder { get; }

        public PhotoContainsHandler(IPhotoFilterBuilder builder, string queryString)
        {
            string[] parts = queryString.Split(' ');
            if (parts[1] == "ct")
            {
                new PhotoPropertyFilterMapper<string>("Name", builder.ApplyNameContainsFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Description", builder.ApplyDescriptionContainsFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Date", builder.ApplyDateContainsFilter, parts[0], parts[2]);
            }
            Builder = builder;
        }
    }
}