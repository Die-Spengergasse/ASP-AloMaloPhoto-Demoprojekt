using Spg.AloMalo.Application;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Linq.Expressions;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class EqualsParameter : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public EqualsParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
            : base("eq")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            ForProperty(queryParameter, p => p.Name)
                .Use<string>((expr, value) => _photoFilterBuilder.EqualsFilter(expr, value));

            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
