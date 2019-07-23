using Newtonsoft.Json;
using RoomNaviMobile.Behaviors;
using RoomNaviMobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace RoomNaviMobile.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimetableView : ContentPage
	{
        public TimetableView(TimetableViewModel viewmodel)
		{
			InitializeComponent ();

            viewmodel.Navigation = Navigation;
            BindingContext = viewmodel;
        }

        private async void OnBarcodeClicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    string strResult = result.Text;
                    txtCampus.Text = strResult.Substring(0,3);
                    txtClassRoom.Text = strResult.Substring(4, strResult.Length - 4);

                    string dayOfWeek = DateTime.Now.DayOfWeek.ToString();

                    switch (dayOfWeek.Substring(0,3))
                    {
                        case "Mon":
                            picker.SelectedIndex = 0;
                            break;
                        case "Tue":
                            picker.SelectedIndex = 1;
                            break;
                        case "Wed":
                            picker.SelectedIndex = 2;
                            break;
                        case "Thu":
                            picker.SelectedIndex = 3;
                            break;
                        case "Fri":
                            picker.SelectedIndex = 4;
                            break;
                        case "Sat":
                            picker.SelectedIndex = 5;
                            break;
                        case "Sun":
                            picker.SelectedIndex = 6;
                            break;


                    }

                    //getTimeTable();
                    btnGetTimetable.Command.Execute(null);

                });
            };
        }
    }
}