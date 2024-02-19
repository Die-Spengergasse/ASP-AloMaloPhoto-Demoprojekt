using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Exceptions
{
    public class PhotoServiceValidationException : ExceptionBase
    {
        private readonly string _message;

        public PhotoServiceValidationException(string message)
        {
            _message = message;
        }

        public static PhotoServiceCreateException FromUsernameExists()
        {
            return new PhotoServiceCreateException("Username exists!");
        }
        public static PhotoServiceCreateException FromLastNameRequired()
        {
            return new PhotoServiceCreateException("Lastname is required!");
        }
        public static PhotoServiceCreateException FromCreationDateTime()
        {
            return new PhotoServiceCreateException("CreationDate must be in future!");
        }
    }
}