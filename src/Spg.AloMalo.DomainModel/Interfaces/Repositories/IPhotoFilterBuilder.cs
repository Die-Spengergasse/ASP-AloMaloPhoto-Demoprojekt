using System;
using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyFilter<TProperty>(Expression<Func<Photo, TProperty>> propertySelector, string value, IFilterOperation<Photo, TProperty> filterOperation);
    }
}