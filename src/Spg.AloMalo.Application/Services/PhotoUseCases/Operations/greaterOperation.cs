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

    public class GreaterThanOperation : PhotoOperationBase
    {
        public GreaterThanOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("gt", photoFilterBuilder) { }

        public override IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ApplyFilter(queryParameter, p => p.Height, PhotoFilterBuilder.ApplyHeightGreaterThanFilter);
            return PhotoFilterBuilder;
        }
    }

    public class GreaterThanEqualOperation : PhotoOperationBase
    {
        public GreaterThanEqualOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("gte", photoFilterBuilder) { }

        public override IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ApplyFilter(queryParameter, p => p.Height, PhotoFilterBuilder.ApplyHeightGreaterThanEqualFilter);
            return PhotoFilterBuilder;
        }
    }
}
