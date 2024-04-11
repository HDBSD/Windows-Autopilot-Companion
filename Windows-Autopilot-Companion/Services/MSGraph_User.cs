using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Maui.Graphics.Platform;
using System.Diagnostics;
using Windows_Autopilot_Companion.Models;


namespace Windows_Autopilot_Companion.Services
{
    public class MSGraph_User
    {
        
        private GraphServiceClient GraphClient;

        public MSGraph_User() 
        {
            var accessTokenProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider());
            GraphClient = new GraphServiceClient(accessTokenProvider, "https://graph.microsoft.com/beta");
        }

        public async Task<User> GetSelf()
        {
            var account = await GraphClient.Me.GetAsync();
            Stream Photo = null;
            ImageSource PhotoSrc = null;
            try
            {
                Photo = await GraphClient.Me.Photo.Content.GetAsync();
                PhotoSrc = ImageUtils.ConvertStreamToImageSource(Photo);
            }
            catch (Microsoft.Graph.Models.ODataErrors.ODataError ex)
            {
                // TODO: need to check json for "code": "ImageNotFound" and handle the error

            }

            User usr = new User()
            {
                UserPrincipalName = account.UserPrincipalName,
                GivenName = account.GivenName,
                Surname = account.Surname,
                Mail = account.Mail,
                DisplayName = account.DisplayName,
                TelephoneNumber = account.MobilePhone,
                ProfileImage = PhotoSrc,
            };

            return usr;
        }
    }

    public class TokenProvider : IAccessTokenProvider
    {
        public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = default,
            CancellationToken cancellationToken = default)
        {
            var token = MSALAuthentication.Instance.AuthResult.AccessToken;

            return Task.FromResult(token);
        }

        public AllowedHostsValidator AllowedHostsValidator { get; }
    }

    public class ImageUtils
    {
        public static ImageSource ConvertStreamToImageSource(Stream stream)
        {
            // Load the stream into a byte array
            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imageData = ms.ToArray();
            }

            // Create an ImageSource from the byte array
            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imageData));

            return imageSource;
        }
    }
}
