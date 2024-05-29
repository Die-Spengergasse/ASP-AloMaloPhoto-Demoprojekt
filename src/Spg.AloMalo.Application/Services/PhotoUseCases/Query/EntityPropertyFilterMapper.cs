using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class EntityPropertyFilterMapper<TEntity, TProperty>
    {
        public string _propertyName;
        public Func<TProperty, TEntity> _filter;

        public EntityPropertyFilterMapper(string propertyName, Func<TProperty, TEntity> filter)
        {
            _propertyName = propertyName;
            _filter = filter;
        }
        public void ExecuteDeligate(TProperty property, string propertyName)
        {
            if (_propertyName == propertyName)
            {
                _filter.Invoke(property);
            }
        }
    }
}

