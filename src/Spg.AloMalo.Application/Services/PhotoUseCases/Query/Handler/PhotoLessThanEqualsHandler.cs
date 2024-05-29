using Spg.AloMalo.Application.Services.PhotoUseCases.Query.Mapper;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Handler
{
    public class PhotoLessThanEqualsHandler
    {
        public IPhotoFilterBuilder Builder { get; }
        public PhotoLessThanEqualsHandler(IPhotoFilterBuilder builder, string queryString)
        {
            string[] parts = queryString.Split(' ');
            if (parts[1] == "lte")
            {
                new PhotoPropertyFilterMapper<string>("Width", builder.ApplyWidthLessThanEqualsFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Height", builder.ApplyWidthLessThanEqualsFilter, parts[0], parts[2]);
            }
            Builder = builder;
        }
    }
}
