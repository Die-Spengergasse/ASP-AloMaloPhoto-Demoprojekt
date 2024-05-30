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
    public class EndsWithOperation : PhotoOperationBase
    {
        public EndsWithOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("ew", photoFilterBuilder) { }

        public override IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ApplyFilter(queryParameter, p => p.Name, PhotoFilterBuilder.ApplyNameEndsWithFilter);
            return PhotoFilterBuilder;
        }
    }
}
