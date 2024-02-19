using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Exceptions
{
    public class AlbumSerivceException : Exception
    {
        public AlbumSerivceException()
             : base() { }

        public AlbumSerivceException(string message)
            : base(message) { }

        public AlbumSerivceException(string message, Exception innerException)
            : base(message, innerException) { }

        public static AlbumSerivceException FromSave()
        {
            return new AlbumSerivceException("Save Album failed!");
        }
    }
}
