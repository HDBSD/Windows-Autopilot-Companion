namespace Windows_Autopilot_Companion
{
	public partial class MainPage : ContentPage
	{
		int count = 0;

		public MainPage()
		{
			InitializeComponent();
		}

		private async void OnCounterClicked(object sender, EventArgs e)
		{
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);

			await Windows_Autopilot_Companion.Services.MSALAuthentication.Instance.Authenticate("e432153a-3392-4262-8176-83b1fe241ad6", new string[] { "User.read.all", "Device.Read.All", "DeviceManagementConfiguration.ReadWrite.All", "DeviceManagementManagedDevices.ReadWrite.All", "DeviceManagementServiceConfig.ReadWrite.All" }, "");
		}
	}

}
