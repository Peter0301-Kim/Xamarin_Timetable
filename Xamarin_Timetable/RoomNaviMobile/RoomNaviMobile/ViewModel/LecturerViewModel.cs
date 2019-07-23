using Newtonsoft.Json;
using RoomNaviMobile.Model;
using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoomNaviMobile.ViewModel
{
    public class LecturerViewModel : ViewModelBase
    {
        ISettingsService _settingsService;

        // private string Url = "https://rntwebservice20190404104209.azurewebsites.net/api/lecturer";

        private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/lecturer";

        Lecturer _lecturer = null;

        private ObservableCollection<Lecturer> _lecturers;



        public LecturerViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            _lecturer = new Lecturer();

            Task.Run(async () => { await GetLecturer(); });
        }

        public string lecturerId
        {
            get { return _lecturer.lecturerId; }
            set {
                _lecturer.lecturerId = value;
                RaisePropertyChanged(() => lecturerId);
            }
        }

        public string givenName
        {
            get { return _lecturer.givenName; }
            set
            {
                _lecturer.givenName = value;
                RaisePropertyChanged(() => givenName);
            }
        }

        public string lastName
        {
            get { return _lecturer.lastName; }
            set
            {
                _lecturer.lastName = value;
                RaisePropertyChanged(() => lastName);
            }
        }

        public string emailAddress
        {
            get { return _lecturer.emailAddress; }
            set
            {
                _lecturer.emailAddress = value;
                RaisePropertyChanged(() => emailAddress);
            }
        }

        public ObservableCollection<Lecturer> lecturers
        {
            get { return _lecturers; }
            set
            {
                _lecturers = value;
                RaisePropertyChanged(() => lecturers);
            }
        }


        private async Task GetLecturer()
        {
            //await DialogService.ShowAlertAsync(lecturerId, "Info", "Close");
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }


            string strUrl = Url + "/" + lecturerId;

            
            HttpClient client = new HttpClient();

            try
            {
                string token = _settingsService.AuthAccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(strUrl);
                string content = result.ToString();
                //Lecturer lecturer = JsonConvert.DeserializeObject<Lecturer>(content);

                List<Lecturer> results = JsonConvert.DeserializeObject<List<Lecturer>>(content);

                lecturerId = results[0].lecturerId;
                givenName = results[0].givenName;
                lastName = results[0].lastName;
                emailAddress = results[0].emailAddress;
                
                //lecturers = new ObservableCollection<Lecturer>(results);

                client.Dispose();
            }
            catch (Exception ex)
            {
                string checkResult = "Error " + ex.ToString();
                client.Dispose();
            }
           
        }

        public ICommand CloseCommand => new Command(async () => await OnClose());

        private async Task OnClose()
        {
            await Navigation.PopModalAsync();
        }
    }
}
