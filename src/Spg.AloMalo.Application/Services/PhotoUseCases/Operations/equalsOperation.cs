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
    public class EqualsOperation : ParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public EqualsOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("eq")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name)
                .Use<string>(_photoFilterBuilder.ApplyNameEqualsFilter);

            ForProperty(queryParameter, p => p.Height)
                .Use<int>(_photoFilterBuilder.ApplyHeightEqualFilter);

            return _photoFilterBuilder;
        }
    }
}
