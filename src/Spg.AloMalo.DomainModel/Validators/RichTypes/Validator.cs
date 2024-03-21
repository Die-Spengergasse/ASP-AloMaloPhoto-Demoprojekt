using Spg.AloMalo.DomainModel.Exceptions;
using System.Data;

namespace Spg.AloMalo.DomainModel.Validators.RichTypes
{
    public class Validator
    {
        public static bool EntityIsValid { get; private set; } = true;

        public static List<string> Errors { get; } = new();

        public static TRichType Validate<TRichType>(TRichType value)
            where TRichType : class, IValidateable<TRichType>, new()
        {
            TRichType richType = new TRichType();
            richType = value;

            (bool isValid, string? error) = richType.IsValid();
            if (!isValid)
            {
                if (error is not null)
                {
                    EntityIsValid = false;
                    Errors.Add(error);
                }
            }
            return richType;
        }

        public static TRichType Validate<TRichType, T>(T value)
            where TRichType : class, IValidateable<TRichType>, IRichType<T>, new()
        {
            TRichType richType = new TRichType();
            richType.Value = value;

            TRichType result = Validate(richType);
            return result;
        }

        public static void Clear()
        {
            //Errors.Clear();
        }

        public static void ThrowOnErrors() 
        {
            if (Errors.Count > 0)
            {
                string errors = Errors.Aggregate((e, d) => $"{e} | ".Trim());
                throw new ValidationException(errors);
            }
        }
    }
}
