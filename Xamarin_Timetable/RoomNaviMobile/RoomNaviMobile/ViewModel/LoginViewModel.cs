using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoomNaviMobile.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private const string Url = "https://identityservice20190424115844.azurewebsites.net/api/RntAppUser/LogIn";
        private const string Url_DbTime = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/DBTIME";

        private string _userId;
        private string _password;
        ISettingsService _settingsService;

        public LoginViewModel(ISettingsService settingsService)
        {
            this._settingsService = settingsService;

        }

        public LoginViewModel()
        {
            
        }

        public string userId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                RaisePropertyChanged(() => userId);
            }
        }

        public string password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => password);
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            /*
            if (navigationData is LogoutParameter)
            {
                var logoutParameter = (LogoutParameter)navigationData;

                if (logoutParameter.Logout)
                {
                    Logout();
                }
            }
            */
            return base.InitializeAsync(navigationData);
        }

        public ICommand LogInCommand => new Command(async () => await OnLogIn());

        private async Task OnLogIn()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            //Execute 2 database service parallelly
            
            Task task1 = LoginService();
            Task task2 = GetDbTime(); // I get db time when login 1. to give user good performance because the first db access is too slow, 2. when db insert, the time must be correspond with db time.

            await task1;
            await task2;

        }

        private async Task LoginService()
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                string sContentType = "application/json";

                JObject inputData = new JObject();
                inputData.Add("UserName", userId);
                inputData.Add("Password", password);
                HttpResponseMessage response = await httpClient.PostAsync(
                    Url,
                    new StringContent(inputData.ToString(), Encoding.UTF8, sContentType));

                string content = await response.Content.ReadAsStringAsync();
                var dic_token = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                string token = dic_token["token"];

                _settingsService.AuthAccessToken = token;

                httpClient.Dispose();
                //goto Main
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                string checkResult = ex.ToString();
                await DialogService.ShowAlertAsync(checkResult, "Error", "Close");
                httpClient.Dispose();
            }
        }
        private async Task GetDbTime()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            HttpClient client = new HttpClient();
            try
            {
                var result = await client.GetStringAsync(Url_DbTime);
                string content = result.ToString();

                client.Dispose();
            }
            catch (Exception ex)
            {
                string checkResult = ex.ToString();
                await DialogService.ShowAlertAsync(checkResult, "Error", "Close");
                client.Dispose();
            }
        }

        public ICommand CancelCommand => new Command(async () => await OnCancel());

        private async Task OnCancel()
        {
            await Navigation.PopAsync();
        }


        
        /*
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}
