using Microsoft.Identity.Client;

namespace Windows_Autopilot_Companion.Services
{
    public enum AuthenticationResultCode
    {
        None,
        Successful,
        Cancelled,
        Denied,
        Unknown
    }

    public partial class MSALAuthentication
    {

        private string error = "";
        private AuthenticationResultCode AuthStatus = AuthenticationResultCode.None;
        private AuthenticationResult? AuthResult = null; 

        public AuthenticationResultCode AuthenticationStatus
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

        public partial Task<AuthenticationResult> Authenticate(string ClientId, string[] Scopes, string RedirectURI);
    }
}