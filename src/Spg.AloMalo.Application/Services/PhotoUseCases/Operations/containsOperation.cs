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
    public class ContainsOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public ContainsOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("ct")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name)
                .Use<string>(_photoFilterBuilder.ApplyNameContainsFilter);

            return _photoFilterBuilder;
        }
    }
}
