using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Exceptions
{
    public class PhotoServiceCreateException : ExceptionBase
    {
        private readonly string _message;

        public PhotoServiceCreateException(string message)
        {
            _message = message;
        }

        public static PhotoServiceCreateException FromSave()
        {
            return new PhotoServiceCreateException("Save Photo failed!");
        }
    }
}
