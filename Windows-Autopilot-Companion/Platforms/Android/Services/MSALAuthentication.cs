using Microsoft.Identity.Client;
using Microsoft.Maui.Platform;

namespace Windows_Autopilot_Companion.Services
{

	public partial class MSALAuthentication
	{
		public async partial Task<AuthenticationResult> Authenticate(string ClientId, string[] Scopes, string RedirectURI)
		{

			var app = PublicClientApplicationBuilder.Create(ClientId)
				/* TODO -- Migrate to proper redirect URI https://learn.microsoft.com/en-us/entra/identity-platform/msal-net-use-brokers-with-xamarin-apps#step-4-add-a-redirect-uri-to-your-app-registration
				 * "The format of the redirect URI for your application depends on the certificate used to sign the APK"
				 * For debugging: While developing, VS signs the APK for you. Error should give the redirect URI you're meant to use, not happening in my case.
				 */
				.WithRedirectUri($"msal{ClientId}://auth")
				.WithBroker()
				.WithParentActivityOrWindow(() => Platform.CurrentActivity)
				.Build();

			var accounts = await app.GetAccountsAsync();
			try
			{
				AuthResult = await app.AcquireTokenSilent(Scopes, accounts.FirstOrDefault()).ExecuteAsync();
				AuthStatus = AuthenticationResultCode.Successful;
			}
			catch (MsalUiRequiredException)
			{
				try
				{
					AuthResult = await app.AcquireTokenInteractive(Scopes).WithUseEmbeddedWebView(true).ExecuteAsync(); //uses embedded browser, attempt to skip the whole redirect URI thing. Will remove.
					AuthStatus = AuthenticationResultCode.Successful;
				}

				catch (MsalException ex)
				{
					AuthStatus = ex.ErrorCode switch
					{
						"authentication_canceled" => AuthenticationResultCode.Cancelled,
						"access_denied" => AuthenticationResultCode.Denied,
						_ => AuthenticationResultCode.Unknown
					};
				}
			}
			return AuthResult;
		}

	}

}