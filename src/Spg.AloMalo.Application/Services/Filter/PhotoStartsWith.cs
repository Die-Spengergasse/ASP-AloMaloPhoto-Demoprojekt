﻿using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class PhotoBeginsWith
    {
        private IPhotoFilterBuilder _builder;

        public PhotoBeginsWith(IPhotoFilterBuilder builder)
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

            new FilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyNameBeginsWithFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);
            new FilterMapper<IPhotoFilterBuilder, string>("Description", _builder.ApplyDescriptionBeginsWithFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}
