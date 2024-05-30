using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{

    public class GreaterThanOperation : ParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public GreaterThanOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("gt")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Height)
                .Use<int>(_photoFilterBuilder.ApplyHeightGreaterThanFilter);

            return _photoFilterBuilder;
        }
    }

    public class GreaterThanEqualOperation : ParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public GreaterThanEqualOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("gte")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Height)
                .Use<int>(_photoFilterBuilder.ApplyHeightGreaterThanEqualFilter);

            return _photoFilterBuilder;
        }
    }
}
