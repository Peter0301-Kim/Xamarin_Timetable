using Newtonsoft.Json;
using RoomNaviMobile.Model;
using RoomNaviMobile.Services;
using RoomNaviMobile.Services.Settings;
using RoomNaviMobile.Services.Subject;
using RoomNaviMobile.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoomNaviMobile.ViewModel
{
    public class SubjectViewModel : ViewModelBase
    {
        ISettingsService _settingsService;
        ISubjectService _subjectService;

        private string _subjectId;
        private ObservableCollection<Subject> _subjects;

        public string subjectId
        {
            get { return _subjectId; }
            set
            {
                _subjectId = value;
                RaisePropertyChanged(() => subjectId);
            }
        }


        public ObservableCollection<Subject> subjects
        {
            get { return _subjects; }
            set
            {
                _subjects = value;
                RaisePropertyChanged(() => subjects);
            }
        }

        public SubjectViewModel()
        {

        }
        public SubjectViewModel(ISettingsService settingsService, ISubjectService subjectService)
        {
            _settingsService = settingsService;
            _subjectService = subjectService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            subjects = await _subjectService.GetAllSubjectsAsync("null","token");

            IsBusy = false;
        }

        public async Task GetAllSubjectsAsyncForTest(string subjectId, string token)
        {
            subjects = await _subjectService.GetAllSubjectsAsync(subjectId, token);
        }

        public ICommand GetSubjectCommand => new Command(async () => await GetSubject());

        private async Task GetSubject()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            string tmpSubjectId = "null";

            if(subjectId == null || subjectId == "")
            {
                tmpSubjectId = "null";
            }
            else
            {
                tmpSubjectId = subjectId;
            }

            try
            {
                subjects = await _subjectService.GetAllSubjectsAsync(tmpSubjectId, _settingsService.AuthAccessToken);
            }
            catch (Exception ex)
            {
                string strErr = "Error " + ex.ToString();


                if (strErr.IndexOf("Unauthorized") > 0)
                {
                    await DialogService.ShowAlertAsync("Please, Log in again.", "Unauthorized", "Close");
                    _settingsService.AuthAccessToken = "";
                }
                else
                {
                    await DialogService.ShowAlertAsync(strErr, "Error", "Close");
                }

                await Navigation.PopAsync();
            }

            

            /*
            HttpClient client = new HttpClient();

            try
            {
                string token = _settingsService.AuthAccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl);
                string content = result.ToString();
                List<Subject> results = JsonConvert.DeserializeObject<List<Subject>>(content);

                subjects = new ObservableCollection<Subject>(results);

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
            */
        }


    }


}
