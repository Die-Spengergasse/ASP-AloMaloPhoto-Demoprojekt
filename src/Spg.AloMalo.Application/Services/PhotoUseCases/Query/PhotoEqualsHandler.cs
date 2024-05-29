using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class PhotoEqualsHandler
    {
        private IPhotoFilterBuilder _builder;

        public PhotoEqualsHandler(IPhotoFilterBuilder builder)
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
            if (queryParts[1] != "eq")
            {
                return _builder;
            }

            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyNameEqualsFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);
            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Description", _builder.ApplyDescriptionEqualsFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);
            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Height", _builder.ApplyHeightEqualsFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}
