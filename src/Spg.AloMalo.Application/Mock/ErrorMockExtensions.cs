using Spg.AloMalo.DomainModel.Error;

namespace Spg.AloMalo.Application.Mock
{
    public static class ErrorMockExtensions
    {
        public static object ResultOrExceptions<TValue, TException1, TException2>(
            this IErrorCheck<TValue> errorCheck,
            Func<TValue, TValue> result,
            Func<TException1, string> error1,
            Func<TException2, string> error2)
            where TException1 : Exception
            where TException2 : Exception
        {
            if (errorCheck.Exception is not null)
            {
                if (errorCheck.Exception is TException1)
                {
                    return error1((TException1)errorCheck.Exception);
                }
                if (errorCheck.Exception is TException2)
                {
                    return error2((TException2)errorCheck.Exception);
                }
            }
            return result(errorCheck.Value)!;
        }
    }
}
