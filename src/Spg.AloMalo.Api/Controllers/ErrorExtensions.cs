using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.DomainModel.Error;

namespace Spg.AloMalo.Api.Controllers
{
    public static class ErrorExtensions
    {
        public static (IActionResult Result, IErrorCheck<TValue> Or) Error<TValue, TException>(
            this IErrorCheck<TValue> errorCheck, 
            Func<TException, IActionResult> error)
                where TException : Exception
        {
            if (errorCheck.Exception is not null)
            {
                if (errorCheck.Exception is TException)
                {
                    return (error((TException)errorCheck.Exception), errorCheck);
                }
            }
            return (null!, errorCheck);
        }

        public static (IActionResult Result, IErrorCheck<TValue> Or) Ok<TValue>(
            this IErrorCheck<TValue> errorCheck,
            Func<TValue, IActionResult> result)
        {
            return (result(errorCheck.Value), errorCheck);
        }

        // Alternativ ohne Return-Tupples und Chaining:

        public static IActionResult ResultOrException<TValue, TException>(
            this IErrorCheck<TValue> errorCheck,
            Func<TValue, IActionResult> result,
            Func<TException, IActionResult> error)
                where TException : Exception
        {
            if (errorCheck.Exception is not null)
            {
                if (errorCheck.Exception is TException)
                {
                    return error((TException)errorCheck.Exception);
                }
            }
            return result(errorCheck.Value);
        }

        public static IActionResult ResultOrExceptions<TValue, TException1, TException2>(
            this IErrorCheck<TValue> errorCheck,
            Func<TValue, IActionResult> result,
            Func<TException1, IActionResult> error1,
            Func<TException2, IActionResult> error2)
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
            return result(errorCheck.Value);
        }
    }
}
