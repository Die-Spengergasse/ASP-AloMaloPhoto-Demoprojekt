using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Validators
{
    public interface IValidateableEntity<TEntity>
    {
        bool IsValid { get; }
        TEntity Validate();
    }
}
