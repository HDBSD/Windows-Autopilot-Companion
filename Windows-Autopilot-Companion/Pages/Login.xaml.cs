using Microsoft.Identity.Client;

namespace Windows_Autopilot_Companion.Pages
{
	public partial class Login : ContentPage
	{

		public Login()
		{
			InitializeComponent();
		}

		private async void OnLoginClicked(object sender, EventArgs e)
		{

			AuthenticationResult Res = await Services.MSALAuthentication.Instance.Authenticate("e432153a-3392-4262-8176-83b1fe241ad6", new string[] { "User.read.all", "Device.Read.All", "DeviceManagementConfiguration.ReadWrite.All", "DeviceManagementManagedDevices.ReadWrite.All", "DeviceManagementServiceConfig.ReadWrite.All" }, "");
			
			LoginBtn.IsVisible = false;
			LogOffBtn.IsVisible = true;
			Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
		}

		private async void OnLogoffClicked(object sender, EventArgs e)
		{
			LoginBtn.IsVisible = true;
			LogOffBtn.IsVisible = false;
			Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
		}

	}

}
