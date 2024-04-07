namespace Windows_Autopilot_Companion.Services
{
    public enum AuthenticationResult
    {
        None,
        Successful,
        Cancelled,
        Denied,
        Unknown
    }

    public partial class MSALAuthentication
    {

        private AuthenticationResult AuthStatus = AuthenticationResult.None;

        public AuthenticationResult AuthenticationStatus
        {
            get {return AuthStatus; }
        }

        private static MSALAuthentication? instance = null;
        public static MSALAuthentication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MSALAuthentication();
                }
                return instance;
            }
        }

        public partial void Authenticate(string ClientId, string[] Scopes, string RedirectURI);
    }
}