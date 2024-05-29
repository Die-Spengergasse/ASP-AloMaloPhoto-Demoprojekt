using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application
{
    public interface IQueryParameter
    {
        IPhotoFilterBuilder Compile(string queryParameter);
    }
}

