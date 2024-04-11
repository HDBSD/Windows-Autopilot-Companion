using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows_Autopilot_Companion.Models;
using Windows_Autopilot_Companion.Services;

namespace Windows_Autopilot_Companion.Viewmodels
{
	public class AccountInfoViewmodel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public AuthenticationResult AuthResult { get; private set; }


        private User _UserProfile;
		public User UserProfile
		{
			get => _UserProfile;
			set
			{
				if (_UserProfile != value)
				{
					_UserProfile = value;
					OnPropertyChanged();
				}
			}
		}

		private bool _loaded = false;
		public bool IsLoaded
		{
			get => _loaded;
			set
			{
				if (_loaded != value)
				{
					_loaded = value;
					OnPropertyChanged();
				}
			}
		}

        public Command LoadUserData { get; set; }

        public AccountInfoViewmodel()
		{
			AuthResult = Services.MSALAuthentication.Instance.AuthResult;

            LoadUserData = new Command(async () => await FetchUserProfileData());
        }

		async Task FetchUserProfileData()
		{
			MSGraph_User MSGraphUserQuery = new MSGraph_User();
            UserProfile = await MSGraphUserQuery.GetSelf();
			IsLoaded = true;

		}

		public void OnPropertyChanged([CallerMemberName] string name="") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
