using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class PhotoGreaterThanFilterHandler
    {
        private IPhotoFilterBuilder _builder;

        public PhotoGreaterThanFilterHandler(IPhotoFilterBuilder builder)
        {
            _builder = builder;
        }

        public IPhotoFilterBuilder WithQuery(string query)
        {
            string[] queryParts = query.Split(' ');
            if (queryParts.Length < 3) {
                throw new Exception("Query has too many Parameters!");
            }
            if (queryParts[1] != "gt")
            {
                return _builder;
            }

            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyHeightGreaterThanFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}
