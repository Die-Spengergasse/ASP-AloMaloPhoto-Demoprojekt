using Spg.AloMalo.Application.Helper;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Operations
{
    public class FilterStartsWithOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterStartsWithOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("sw")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name).Use<string>(_photoFilterBuilder.ApplyNameStartsWithFilter);
            return _photoFilterBuilder;
        }
    }
}