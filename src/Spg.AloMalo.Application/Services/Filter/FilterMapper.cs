using System;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class FilterMapper<TEntity, TProperty>
    {
        private string _shouldPropertyName;
        private Func<TProperty, TEntity> _logic;

        public FilterMapper(string shouldPropertyName, Func<TProperty, TEntity> logic)
        {
            _shouldPropertyName = shouldPropertyName;
            _logic = logic;
        }

        public void ExecuteDeligateIfValid(string propertyName, TProperty propertyValue)
        {
            if (propertyName == _shouldPropertyName)
            {
                _logic.Invoke(propertyValue);
            }
        }
    }
}
