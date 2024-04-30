namespace Spg.General.IdentityProvider.Services
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message)
            : base(message)
        { }

        public static AuthenticationException FromWrongCredentials()
        {
            return new AuthenticationException("Username der Passwort falsch!");
        }
    }
}
