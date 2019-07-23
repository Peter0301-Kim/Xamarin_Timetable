using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.View;
using RoomNaviMobile.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoomNaviMobile.ViewModel
{
    public class MainViewModel: ViewModelBase
    {

        ISettingsService _settingsService;
        public MainViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;


        }

        //public ICommand SettingsCommand => new Command(async () => await SettingsAsync());

        /*
        private async Task SettingsAsync()
        {

        }
        */

        public ICommand LoginCommand => new Command(async () => await OnLoginCommand());
        private async Task OnLoginCommand()
        {
            var viewModel = DependencyInjector.Resolve<LoginViewModel>();
            await Navigation.PushAsync(new LoginView(viewModel));

        }

        public ICommand LogoutCommand => new Command( () =>  OnLogoutCommand());
        private void OnLogoutCommand()
        {
            _settingsService.AuthAccessToken = "";

        }

        public ICommand SubjectCommand => new Command(async () => await OnSubjectCommand());
        private async Task OnSubjectCommand()
        {
            if(_settingsService.AuthAccessToken == "")
            {
                await OnLoginCommand();
            }
            else
            {
                var viewModel = DependencyInjector.Resolve<SubjectViewModel>();

                await Navigation.PushAsync(new SubjectView(viewModel));
            }

        }

        public ICommand TimetableCommand => new Command(async () => await OnTimetableCommand());
        private async Task OnTimetableCommand()
        {
            if (_settingsService.AuthAccessToken == "")
            {
                await OnLoginCommand();
            }
            else
            {
                var viewModel = DependencyInjector.Resolve<TimetableViewModel>();
                await Navigation.PushAsync(new TimetableView(viewModel));
            }
        }

        public ICommand LecturerTimetableCommand => new Command(async () => await OnLecturerTimetableCommand());
        private async Task OnLecturerTimetableCommand()
        {
            if (_settingsService.AuthAccessToken == "")
            {
                await OnLoginCommand();
            }
            else
            {
                var viewModel = DependencyInjector.Resolve<LecturerTimetableViewModel>();
                await Navigation.PushAsync(new LecturerTimetableView(viewModel));
            }
        }

        public ICommand CourseCommand => new Command(async () => await OnCourseCommand());
        private async Task OnCourseCommand()
        {
            string uri = "https://www.tafesa.edu.au/courses";
            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.AliceBlue,
                PreferredControlColor = Color.Violet
            });

        }

        public ICommand LocationCommand => new Command(async () => await OnLocationCommand());
        private async Task OnLocationCommand()
        {
            string uri = "https://www.tafesa.edu.au/locations";

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.AliceBlue,
                PreferredControlColor = Color.Violet
            });
        }

    }







}
