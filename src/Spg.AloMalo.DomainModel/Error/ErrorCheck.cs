namespace Spg.AloMalo.DomainModel.Error
{
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators
    public class ErrorCheck<TValue> : IErrorCheck<TValue>
    {
        private readonly TValue? _value = default;
        private readonly Exception? _exception;

        public ErrorCheck(TValue value)
        {
            _value = value;
        }
        public ErrorCheck(Exception exception)
        {
            _exception = exception;
        }

        public TValue Value => _value!;
        public Exception? Exception => _exception;

        public static implicit operator ErrorCheck<TValue>(TValue value)
        {
            return new ErrorCheck<TValue>(value);
        }

        public static implicit operator ErrorCheck<TValue>(Exception exception)
        {
            return new ErrorCheck<TValue>(exception);
        }
    }
}
