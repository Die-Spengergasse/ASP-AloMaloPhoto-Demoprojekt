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
    public class EqualsOperation : PhotoOperationBase
    {
        public EqualsOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("eq", photoFilterBuilder) { }

        public override IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ApplyFilter(queryParameter, p => p.Name, PhotoFilterBuilder.ApplyNameEqualsFilter);
            ApplyFilter(queryParameter, p => p.Height, PhotoFilterBuilder.ApplyHeightEqualFilter);
            return PhotoFilterBuilder;
        }
    }
}
