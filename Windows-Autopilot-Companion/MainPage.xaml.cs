using Microsoft.Identity.Client;

namespace Windows_Autopilot_Companion
{
	public partial class MainPage : ContentPage
	{

		public MainPage()
		{
			InitializeComponent();
		}

		private async void OnCounterClicked(object sender, EventArgs e)
		{

			AuthenticationResult Res = await Services.MSALAuthentication.Instance.Authenticate("e432153a-3392-4262-8176-83b1fe241ad6", new string[] { "User.read.all", "Device.Read.All", "DeviceManagementConfiguration.ReadWrite.All", "DeviceManagementManagedDevices.ReadWrite.All", "DeviceManagementServiceConfig.ReadWrite.All" }, "");
			
			CounterBtn.Text = $"Clicked - {res.Account}";

		}
	}

}
