using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.View;
using RoomNaviMobile.ViewModel;
using RoomNaviMobile.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoomNaviMobile.View
{
    public partial class MainView : ContentPage
    {
        public MainView(MainViewModel viewmodel)
        {
            InitializeComponent();

            viewmodel.Navigation = Navigation;
            BindingContext = viewmodel;
        }
    }
}
