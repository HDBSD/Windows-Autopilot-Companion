using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Autopilot_Companion.Viewmodels
{
	public class AccountInfoViewmodel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public AuthenticationResult AuthResult { get; private set; }

		public AccountInfoViewmodel()
		{
			AuthResult = Services.MSALAuthentication.Instance.AuthResult;
		}


		public void OnPropertyChanged([CallerMemberName] string name="") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
