using Microsoft.Identity.Client;
using Windows_Autopilot_Companion.Viewmodels;

namespace Windows_Autopilot_Companion.Pages
{
	public partial class AccountInfo : ContentPage
	{

		//public static AuthenticationResult? AuthResult { get; private set; }

		//AccountInfoViewmodel viewmodel;
		public AccountInfo()
		{
			InitializeComponent();
			//BindingContext = viewmodel = new AccountInfoViewmodel();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

			var viewmodel = (AccountInfoViewmodel)BindingContext;

			if (viewmodel.IsLoaded == false)
				viewmodel.LoadUserData.Execute(null);
        }
    }
}
