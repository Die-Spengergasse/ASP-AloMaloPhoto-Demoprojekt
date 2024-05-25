using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public interface IQueryParameter
    {
        IPhotoFilterBuilder Compile(IPhotoFilterBuilder builder, string queryParameter);
    }
}
