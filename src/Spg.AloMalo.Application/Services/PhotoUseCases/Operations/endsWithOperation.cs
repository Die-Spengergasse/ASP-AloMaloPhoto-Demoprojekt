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
    public class EndsWithOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public EndsWithOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("ew")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name)
                .Use<string>(_photoFilterBuilder.ApplyNameEndsWithFilter);

            return _photoFilterBuilder;
        }
    }
}
