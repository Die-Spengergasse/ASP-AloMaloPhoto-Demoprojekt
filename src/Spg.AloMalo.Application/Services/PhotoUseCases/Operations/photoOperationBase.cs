using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{
    public abstract class PhotoOperationBase : ParameterBase<Photo>, IQueryParameter
    {
        protected readonly IPhotoFilterBuilder PhotoFilterBuilder;

        protected PhotoOperationBase(string @operator, IPhotoFilterBuilder photoFilterBuilder)
            : base(@operator)
        {
            PhotoFilterBuilder = photoFilterBuilder ?? throw new ArgumentNullException(nameof(photoFilterBuilder));
        }

        public abstract IPhotoFilterBuilder Compile(string? queryParameter);
    }
}
