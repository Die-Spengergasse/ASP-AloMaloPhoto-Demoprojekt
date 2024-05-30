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
    public class LowerThanOperation : ParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public LowerThanOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("lt")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Height)
                .Use<int>(_photoFilterBuilder.ApplyHeightLowerThanFilter);

            return _photoFilterBuilder;
        }
    }

    public class LowerThanEqualOperation : ParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public LowerThanEqualOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("lte")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Height)
                .Use<int>(_photoFilterBuilder.ApplyHeightLowerThanEqualFilter);

            return _photoFilterBuilder;
        }
    }
}
