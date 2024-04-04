using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Exceptions
{
    public class PhotoServiceCreateException : Exception
    {
        public PhotoServiceCreateException()
             : base() { }

        public PhotoServiceCreateException(string message)
            : base(message) { }

        public PhotoServiceCreateException(string message, Exception innerException)
            : base(message, innerException) { }

        public static PhotoServiceCreateException FromSave(Exception innerException)
        {
            return new PhotoServiceCreateException("Save Photo failed!", innerException);
        }
    }
}
