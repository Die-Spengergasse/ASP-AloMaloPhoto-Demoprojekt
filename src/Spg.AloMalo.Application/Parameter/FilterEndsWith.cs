using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Parameter
{
    public class FilterEndsWith : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        public FilterEndsWith(IPhotoFilterBuilder photoFilterBuilder) : base("ew")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name).Use<string>(_photoFilterBuilder.ApplyNameEndsWithFilter);
            ForProperty(queryParameter, p => p.Description).Use<string>(_photoFilterBuilder.ApplyDescriptionEndssFilter);

            return _photoFilterBuilder;
        }
    }
}

