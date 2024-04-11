using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

namespace Windows_Autopilot_Companion.Services
{

    public partial class MSALAuthentication
    {
        public async partial Task<AuthenticationResult> Authenticate(string ClientId, string[] Scopes, string RedirectURI)
        {

            var storageProperties =
                new StorageCreationPropertiesBuilder("windows_autopilot_companion_msalcache", MsalCacheHelper.UserRootDirectory)
                .Build();

            var app = PublicClientApplicationBuilder.Create(ClientId)
                .WithRedirectUri($"msal{ClientId}://auth")
                .WithBroker()
                .WithParentActivityOrWindow(GetWindowHwnd)
                .Build();

            var cacheHelper = await MsalCacheHelper.CreateAsync(storageProperties);
            cacheHelper.RegisterCache(app.UserTokenCache);

            try
            {

                var accounts = await app.GetAccountsAsync();
                var result = await app.AcquireTokenSilent(Scopes, accounts.FirstOrDefault())
                                    .ExecuteAsync();

                AuthResult = result;
                AuthStatus = AuthenticationResultCode.Successful;
            }
            catch (MsalUiRequiredException ex)
            {
                try
                {

                    var result = await app.AcquireTokenInteractive(Scopes)
                                        .ExecuteAsync();

                    AuthResult = result;
                    AuthStatus = AuthenticationResultCode.Successful;
                }
                catch (MsalException msalEx)
                {
                    switch (msalEx.ErrorCode)
                    {
                        case "authentication_canceled":
                            AuthStatus = AuthenticationResultCode.Cancelled;
                            break;
                        case "access_denied":
                            AuthStatus = AuthenticationResultCode.Denied;
                            break;
                        default:
                            AuthStatus = AuthenticationResultCode.Unknown;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AuthStatus = AuthenticationResultCode.Unknown;
                error = ex.Message + " " + ex.StackTrace;
            }

            return AuthResult;
        }

        public IntPtr GetWindowHwnd()
        {
            return ((MauiWinUIWindow)App.Current.Windows[0].Handler.PlatformView).WindowHandle;  
        }
    }

}