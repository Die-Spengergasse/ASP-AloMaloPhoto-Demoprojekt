using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class PhotoEndsWithHandler
    {
        private IPhotoFilterBuilder _builder;

        public PhotoEndsWithHandler(IPhotoFilterBuilder builder)
        {
            _builder = builder;
        }

        public IPhotoFilterBuilder WithQuery(string query)
        {
            string[] queryParts = query.Split(' ');
            if (queryParts.Length > 3)
            {
                throw new Exception("Query has to many Parameters!");
            }
            if (queryParts[1] != "ew")
            {
                return _builder;
            }

            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyNameEndsWithFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);
            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Description", _builder.ApplyDescriptionEndsWithFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}
