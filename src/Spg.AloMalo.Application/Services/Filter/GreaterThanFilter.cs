using System;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class GreaterThanFilter<T> : IFilterOperation<T> where T : IComparable<T>
    {
        public Func<T, bool> GetFilter(string propertyValue)
        {
            T value;
            try
            {
                value = (T)Convert.ChangeType(propertyValue, typeof(T));
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException($"Value '{propertyValue}' cannot be converted to type {typeof(T)}");
            }
            catch (FormatException)
            {
                throw new ArgumentException($"Value '{propertyValue}' is in an invalid format for type {typeof(T)}");
            }
            catch (OverflowException)
            {
                throw new ArgumentException($"Value '{propertyValue}' is too large to be converted to type {typeof(T)}");
            }

            return item => item.CompareTo(value) > 0;
        }
    }
}