using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoomNaviMobile.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		public LoginView(LoginViewModel viewmodel)
		{
			InitializeComponent ();

            viewmodel.Navigation = Navigation;
            BindingContext = viewmodel;
        }
    }
}