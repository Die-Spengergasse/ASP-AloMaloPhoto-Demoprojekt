using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IFilterOperation<T, TProperty>
    {
        Expression<Func<T, bool>> Apply(Expression<Func<T, TProperty>> propertySelector, string value);
    }
}