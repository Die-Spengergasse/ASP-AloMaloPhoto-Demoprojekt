using Spg.AloMalo.Application.Services.PhotoUseCases.Query.Mapper;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Handler
{
    public class PhotoLessThanHandler
    {
        public IPhotoFilterBuilder Builder { get; }
        public PhotoLessThanHandler(IPhotoFilterBuilder builder, string queryString)
        {
            string[] parts = queryString.Split(' ');
            if (parts[1] == "lt")
            {
                new PhotoPropertyFilterMapper<string>("Width", builder.ApplyWidthLessThanFilter, parts[0], parts[2]);
                new PhotoPropertyFilterMapper<string>("Height", builder.ApplyHeightLessThanFilter, parts[0], parts[2]);
            }
            Builder = builder;
        }
    }
}