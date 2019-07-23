using Newtonsoft.Json;
using RoomNaviMobile.Model;
using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.View;
using RoomNaviMobile.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoomNaviMobile.ViewModel
{
    public class TimetableViewModel : ViewModelBase
    {
        ISettingsService _settingsService;

        private string _campus;
        private string _classroom;
        private List<string> _dayofweek;
        private string _selectedDayOfWeek;
        private ObservableCollection<Timetable> _timetables;

        //private string Url = "https://rntwebservice20190404104209.azurewebsites.net/api/Timetable";

        private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/Timetable";

        public TimetableViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            SetDayOfWeek();
        }

        public void SetDayOfWeek()
        {
            var weekList = new List<string>();
            weekList.Add("Mon");
            weekList.Add("Tue");
            weekList.Add("Wed");
            weekList.Add("Thu");
            weekList.Add("Fri");
            weekList.Add("Sat");
            weekList.Add("Sun");

            dayofweek = weekList;
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

        public string campus
        {
            get { return _campus; }
            set
            {
                _campus = value;
                RaisePropertyChanged(() => campus);
            }
        }

        public string classroom
        {
            get { return _classroom; }
            set
            {
                _classroom = value;
                RaisePropertyChanged(() => classroom);
            }
        }

        public List<string> dayofweek
        {
            get { return _dayofweek; }
            set
            {
                _dayofweek = value;
                RaisePropertyChanged(() => dayofweek);
            }
        }

        public string selectedDayOfWeek
        {
            get { return _selectedDayOfWeek; }
            set
            {
                _selectedDayOfWeek = value;
                RaisePropertyChanged(() => selectedDayOfWeek);
            }
        }

        public ICommand GetTimetableCommand => new Command(async () => await GetTimetable());

        private async Task GetTimetable()
        {
            if(campus == "" || campus == null)
            {
                await DialogService.ShowAlertAsync("Please enter campus", "Not Valid", "Close");
                
                return;
            }

            if (classroom == "" || classroom == null)
            {
                await DialogService.ShowAlertAsync("Please enter classroom", "Not Valid", "Close");
                return;
            }

            if (selectedDayOfWeek == "" || selectedDayOfWeek == null)
            {
                await DialogService.ShowAlertAsync("Please choose a day of week", "Not Valid", "Close");
                return;
            }

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            string destUrl = Url + "/" + campus + "/" + classroom + "/" + selectedDayOfWeek;

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


        public ICommand ShowLecturerCommand => new Command<Timetable>(async (item) => await OnShowLecturerCommand(item));
        private async Task OnShowLecturerCommand(Timetable timetable)
        {
            string msg = "Campus" + timetable.lecturerId;


            var viewModel = DependencyInjector.Resolve<LecturerViewModel>();
            viewModel.lecturerId = timetable.lecturerId;
            await Navigation.PushModalAsync(new LecturerView(viewModel));
        }


    }
}
