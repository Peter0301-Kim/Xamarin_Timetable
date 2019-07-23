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
	public partial class LecturerTimetableView : ContentPage
	{
		public LecturerTimetableView (LecturerTimetableViewModel viewmodel)
		{
			InitializeComponent ();

            viewmodel.Navigation = Navigation;
            BindingContext = viewmodel;
        }
	}
}