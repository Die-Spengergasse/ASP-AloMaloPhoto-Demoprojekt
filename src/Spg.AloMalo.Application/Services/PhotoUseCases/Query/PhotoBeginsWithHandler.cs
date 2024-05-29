using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class PhotoBeginsWithHandler
    {
        private IPhotoFilterBuilder _builder;

        public PhotoBeginsWithHandler(IPhotoFilterBuilder builder)
        {
            _builder = builder;
        }

        public IPhotoFilterBuilder WithQuery(string query)
        {
            string[] queryParts = query.Split(' ');

            if (queryParts[1] != "bw")
            {
                return _builder;
            }

            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyNameBeginsWithFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);
            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Description", _builder.ApplyDescriptionBeginsWithFilter)
                .ExecuteDeligate(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}
