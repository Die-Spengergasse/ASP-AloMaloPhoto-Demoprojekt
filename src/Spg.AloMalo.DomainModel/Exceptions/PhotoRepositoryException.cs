using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Exceptions
{
    public class PhotoRepositoryException : Exception
    {
        public PhotoRepositoryException()
            : base()
        { }

        public PhotoRepositoryException(string message)
            : base(message)
        { }

        public PhotoRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public static PhotoRepositoryException FromCreate(Exception ex)
        {
            return new PhotoRepositoryException("Method Create failed!", ex);
        }
        public static PhotoRepositoryException FromDelete()
        {
            return FromDelete(default!);
        }
        public static PhotoRepositoryException FromDelete(Exception ex)
        {
            return new PhotoRepositoryException("Method Delete failed!", ex);
        }
        public static PhotoRepositoryException FromUpdate(Exception ex)
        {
            return new PhotoRepositoryException("Method Update failed!", ex);
        }
    }
}
