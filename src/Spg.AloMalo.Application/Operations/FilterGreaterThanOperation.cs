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
    public class FilterGreaterThanOperation : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterGreaterThanOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("gt")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            // Hier filtere ich nach einem numerischen Wert, z.B. Photo.Height, der größer als der angegebene Parameter ist.
            ForProperty(queryParameter, p => p.Height).Use<int>(_photoFilterBuilder.ApplyGreaterThanFilter);
            return _photoFilterBuilder;
        }
    }
}
