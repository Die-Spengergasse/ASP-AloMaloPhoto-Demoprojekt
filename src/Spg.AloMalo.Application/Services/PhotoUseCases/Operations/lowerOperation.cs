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
    public class LowerThanOperation : PhotoOperationBase
    {
        public LowerThanOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("lt", photoFilterBuilder) { }

        public override IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ApplyFilter(queryParameter, p => p.Height, PhotoFilterBuilder.ApplyHeightLowerThanFilter);
            return PhotoFilterBuilder;
        }
    }

    public class LowerThanEqualOperation : PhotoOperationBase
    {
        public LowerThanEqualOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("lte", photoFilterBuilder) { }

        public override IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ApplyFilter(queryParameter, p => p.Height, PhotoFilterBuilder.ApplyHeightLowerThanEqualFilter);
            return PhotoFilterBuilder;
        }
    }
}
