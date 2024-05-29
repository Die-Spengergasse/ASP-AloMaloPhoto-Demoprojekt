﻿using System;
using Spg.AloMalo.Application;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Parameter
{
	public class FilterEquals : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        public FilterEquals(IPhotoFilterBuilder photoFilterBuilder) : base("eq")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Height).Use<int>(_photoFilterBuilder.ApplyHightEquals);

            return _photoFilterBuilder;
        }
    }
}

