using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Parameter
{
    public class FilterStartsWith : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        public FilterStartsWith(IPhotoFilterBuilder photoFilterBuilder) : base("bw")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name).Use<string>(_photoFilterBuilder.ApplyNameBeginsWithFilter);

            return _photoFilterBuilder;
        }
    }
}

