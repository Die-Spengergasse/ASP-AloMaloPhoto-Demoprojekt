using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Exceptions
{
    public class PhotoServiceUpdateException : ExceptionBase
    {
        private readonly string _message;

        public PhotoServiceUpdateException(string message)
        {
            _message = message;
        }

        public static PhotoServiceUpdateException FromSave()
        {
            return new PhotoServiceUpdateException("Save Photo failed!");
        }
    }
}
