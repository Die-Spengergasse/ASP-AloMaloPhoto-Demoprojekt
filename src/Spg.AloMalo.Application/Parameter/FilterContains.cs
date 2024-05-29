using System;
using System.Reflection;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Parameter
{
	public class FilterContains: InterpretParameterBase<Photo>, IQueryParameter
	{
		private readonly IPhotoFilterBuilder _photoFilterBuilder;
		public FilterContains(IPhotoFilterBuilder photoFilterBuilder) : base("ct")
		{
            _photoFilterBuilder = photoFilterBuilder;
        }
		public IPhotoFilterBuilder Compile(string? queryParameter)
		{
			ForProperty(queryParameter, p => p.Name).Use<string>(_photoFilterBuilder.ApplyNameContainsFilter);
            ForProperty(queryParameter, p => p.Description).Use<string>(_photoFilterBuilder.ApplyDescriptionContainsFilter);

            return _photoFilterBuilder;
		}
    }
}

