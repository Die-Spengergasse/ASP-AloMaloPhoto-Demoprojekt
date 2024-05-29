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
    public class FilterLessThanOperation : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterLessThanOperation(IPhotoFilterBuilder photoFilterBuilder)
            : base("lt")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            // Hier filtere ich nach einem numerischen Wert, z.B. Photo.Size, der kleiner als der angegebene Parameter ist.
            ForProperty(queryParameter, p => p.Height).Use<int>(_photoFilterBuilder.ApplyLessThanFilter);
            return _photoFilterBuilder;
        }
    }
}