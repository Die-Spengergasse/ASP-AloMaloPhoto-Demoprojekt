using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Model.RichTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Spg.AloMalo.DomainModel.Validators.RichTypes
{
    public abstract record RichTypeBase<TEntity, T> : IRichType<T>
        where TEntity : class, IValidateable<TEntity>, new()
    {
        public T? Value { get; set; }

        public RichTypeBase()
        { }
        public RichTypeBase(T value)
        {
            Value = value;
            (bool isValid, string? error) result = IsValid();
            if (!result.isValid)
            {
                throw new ValidationException(result.error ?? "");
            }
        }

        public abstract (bool, string?) IsValid();
    }
}
