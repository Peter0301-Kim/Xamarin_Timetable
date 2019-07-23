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
    public class LecturerTimetableViewModel : ViewModelBase
    {
        ISettingsService _settingsService;

        private string _lecturerName;

        private bool _isMon;
        private bool _isTue;
        private bool _isWed;
        private bool _isThu;
        private bool _isFri;

        private ObservableCollection<Timetable> _timetables;

        private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/LecturerTimetable";

        public LecturerTimetableViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            isMon = true;

        }

        public ObservableCollection<Timetable> timetables
        {
            get { return _timetables; }
            set
            {
                _timetables = value;
                RaisePropertyChanged(() => timetables);
            }

        }

        public string lecturerName
        {
            get { return _lecturerName; }
            set
            {
                _lecturerName = value;
                RaisePropertyChanged(() => lecturerName);
            }
        }

        public bool isMon
        {
            get { return _isMon; }
            set
            {
                if(!value && !isTue && !isWed && !isThu && !isFri)
                {
                    _isMon = true;
                    RaisePropertyChanged(() => isMon);
                    return;
                }
                _isMon = value;
                RaisePropertyChanged(() => isMon);
            }
        }

        public bool isTue
        {
            get { return _isTue; }
            set
            {
                if (!value && !isMon && !isWed && !isThu && !isFri)
                {
                    _isTue = true;
                    RaisePropertyChanged(() => isTue);
                    return;
                }
                _isTue = value;
                RaisePropertyChanged(() => isTue);
            }
        }

        public bool isWed
        {
            get { return _isWed; }
            set
            {
                if (!value && !isMon && !isTue && !isThu && !isFri)
                {
                    _isWed = true;
                    RaisePropertyChanged(() => isWed);
                    return;
                }
                _isWed = value;
                RaisePropertyChanged(() => isWed);
            }
        }

        public bool isThu
        {
            get { return _isThu; }
            set
            {
                if (!value && !isMon && !isTue && !isWed && !isFri)
                {
                    _isThu = true;
                    RaisePropertyChanged(() => isThu);
                    return;
                }
                _isThu = value;
                RaisePropertyChanged(() => isThu);
            }
        }

        public bool isFri
        {
            get { return _isFri; }
            set
            {
                if (!value && !isMon && !isTue && !isWed && !isThu)
                {
                    _isFri = true;
                    RaisePropertyChanged(() => isFri);
                    return;
                }
                _isFri = value;
                RaisePropertyChanged(() => isFri);
            }
        }

        public ICommand GetLecturerTimetableCommand => new Command(async () => await GetLecturerTimetable());

        private async Task GetLecturerTimetable()
        {
            if(lecturerName == "" || lecturerName == null)
            {
                await DialogService.ShowAlertAsync("Please enter lecturer's name", "Info", "Close");
                return;
            }

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            string dayOfWeeks = "";


            if (isMon) { dayOfWeeks += "1,"; }
            if (isTue) { dayOfWeeks += "2,"; }
            if (isWed) { dayOfWeeks += "3,"; }
            if (isThu) { dayOfWeeks += "4,"; }
            if (isFri) { dayOfWeeks += "5,"; }

            dayOfWeeks = dayOfWeeks.Remove(dayOfWeeks.Length - 1);


            string destUrl = Url + "/" + lecturerName + "/" + dayOfWeeks;

            HttpClient client = new HttpClient();
            try
            {

                string token = _settingsService.AuthAccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl);
                string content = result.ToString();
                List<Timetable> timeTables = JsonConvert.DeserializeObject<List<Timetable>>(content);

                timetables = new ObservableCollection<Timetable>(timeTables);

                client.Dispose();

            }
            catch (Exception ex)
            {
                string checkResult = "Error " + ex.ToString();
                await DialogService.ShowAlertAsync(checkResult, "Error", "Close");

                if (checkResult.IndexOf("Unauthorized") > 0)
                {
                    _settingsService.AuthAccessToken = "";
                    client.Dispose();

                    await Navigation.PopAsync();
                    return;

                }
                client.Dispose();
            }
        }

    }
}
