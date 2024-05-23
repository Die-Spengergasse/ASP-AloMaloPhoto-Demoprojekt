using System;
using System.Text.RegularExpressions;

namespace Spg.AloMalo.Repository.Filters
{
    public class GenericFilter<T>
    {
        public Func<T, bool> EqualsFilter(string propertyName, object value)
        {
            return item => item.GetType().GetProperty(propertyName)?.GetValue(item)?.Equals(value) ?? false;
        }

        public Func<T, bool> StartsWithFilter(string propertyName, string value)
        {
            return item => item.GetType().GetProperty(propertyName)?.GetValue(item)?.ToString().StartsWith(value) ?? false;
        }

        public Func<T, bool> EndsWithFilter(string propertyName, string value)
        {
            return item => item.GetType().GetProperty(propertyName)?.GetValue(item)?.ToString().EndsWith(value) ?? false;
        }

        public Func<T, bool> ContainsFilter(string propertyName, string value)
        {
            return item => item.GetType().GetProperty(propertyName)?.GetValue(item)?.ToString().Contains(value) ?? false;
        }

        public Func<T, bool> RegexFilter(string propertyName, string pattern)
        {
            return item => Regex.IsMatch(item.GetType().GetProperty(propertyName)?.GetValue(item)?.ToString() ?? "", pattern);
        }

        public Func<T, bool> ContainsDigitsFilter(string propertyName)
        {
            return item => Regex.IsMatch(item.GetType().GetProperty(propertyName)?.GetValue(item)?.ToString() ?? "", @"\d");
        }

        public Func<T, bool> NotFilter(Func<T, bool> filter)
        {
            return item => !filter(item);
        }

        public Func<T, bool> GreaterThanFilter(string propertyName, IComparable value)
        {
            return item => Comparer<IComparable>.Default.Compare(item.GetType().GetProperty(propertyName)?.GetValue(item) as IComparable, value) > 0;
        }

        public Func<T, bool> LessThanFilter(string propertyName, IComparable value)
        {
            return item => Comparer<IComparable>.Default.Compare(item.GetType().GetProperty(propertyName)?.GetValue(item) as IComparable, value) < 0;
        }

        public Func<T, bool> BetweenFilter(string propertyName, IComparable lowerValue, IComparable upperValue)
        {
            return item =>
            {
                var propertyValue = item.GetType().GetProperty(propertyName)?.GetValue(item) as IComparable;
                return Comparer<IComparable>.Default.Compare(propertyValue, lowerValue) >= 0 &&
                       Comparer<IComparable>.Default.Compare(propertyValue, upperValue) <= 0;
            };
        }
    }
}
