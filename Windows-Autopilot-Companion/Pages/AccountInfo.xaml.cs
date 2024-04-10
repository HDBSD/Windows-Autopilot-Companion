using Microsoft.Identity.Client;

namespace Windows_Autopilot_Companion.Pages
{
	public partial class AccountInfo : ContentPage
	{

		public static AuthenticationResult? AuthResult { get; private set; }
		public AccountInfo()
		{
			InitializeComponent();

		}
	}
}
