using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public abstract class User
    {
        //public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        #region -- Login ----------------------------------------------
        public EMail Username { get; private set; } = default!;
        public string Password { get; set; } = string.Empty;
        #endregion -- Login ----------------------------------------------

        protected User()
        { }
        protected User(string firstName, string lastName, EMail username)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
        }
    }
}
