using Microsoft.Identity.Client;

namespace Windows_Autopilot_Companion.Services
{

	public partial class MSALAuthentication
	{
		public async partial Task<AuthenticationResult> Authenticate(string ClientId, string[] Scopes, string RedirectURI)
		{
			throw new NotImplementedException();
		}
	}

}