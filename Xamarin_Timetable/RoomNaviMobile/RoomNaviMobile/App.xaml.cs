using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.ViewModel.Base;
using System.Threading.Tasks;
using RoomNaviMobile.View;
using RoomNaviMobile.ViewModel;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RoomNaviMobile
{
    public partial class App : Application
    {
        ISettingsService _settingsService;
        public App()
        {
            InitializeComponent();
            
            //RegisterDependency();

            _settingsService = DependencyInjector.Resolve<ISettingsService>();

            var viewModel = DependencyInjector.Resolve<MainViewModel>();

            MainPage = new NavigationPage(new MainView(viewModel)) {
                BarBackgroundColor = (Color)Application.Current.Resources["PageBackgroundColor"]

            };
        }

        private void RegisterDependency()
        {

        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            base.OnStart();

            /*
            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }
            if (_settingsService.AllowGpsLocation && !_settingsService.UseFakeLocation)
            {
                //await GetGpsLocation();
            }
            if (!_settingsService.UseMocks && !string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            {
                //await SendCurrentLocation();
            }
            */
            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //_settingsService.AuthAccessToken = "";
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
