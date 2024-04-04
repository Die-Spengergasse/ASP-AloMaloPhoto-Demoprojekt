using Spg.AloMalo.DomainModel.Model.RichTypes;
using Spg.AloMalo.DomainModel.Validators.RichTypes;

namespace Spg.AloMalo.DomainModel.Model
{
    public abstract class User
    {
        public string FirstName { get; set; } = string.Empty!;
        //public FirstName FirstName { get; set; } = default!;

        public string LastName { get; set; } = string.Empty!;

        #region -- Login ----------------------------------------------
        public EMail Username { get; private set; } = default!;
        public string Password { get; set; } = string.Empty;
        #endregion -- Login ----------------------------------------------

        protected User()
        { }
        protected User(string firstName, string lastName, EMail username)
        {
            //Validator.Clear();
            //FirstName = Validator.Validate<FirstName, string>(firstName);
            //Username = username;

            //Validator.ThrowOnErrors();

            FirstName = firstName;
            LastName = lastName;
            Username = username;
        }
    }
}
