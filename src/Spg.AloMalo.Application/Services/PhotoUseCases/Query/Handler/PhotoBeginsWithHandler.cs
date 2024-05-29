using Spg.AloMalo.Application.Services.PhotoUseCases.Query.Mapper;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Handler
{
    public class PhotoBeginsWithHandler
    {
        public IPhotoFilterBuilder Builder { get; }
        public PhotoBeginsWithHandler(IPhotoFilterBuilder builder, string queryString)
        {
            string[] parts = queryString.Split(' ');
            if (parts[1] == "bw")
            {
                new PhotoPropertyFilterMapper<string>("Name", builder.ApplyNameBeginsWithFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Description", builder.ApplyDescriptionBeginsWithFilter, parts[0], parts[2]);
            }
            Builder = builder;
        }
    }
}