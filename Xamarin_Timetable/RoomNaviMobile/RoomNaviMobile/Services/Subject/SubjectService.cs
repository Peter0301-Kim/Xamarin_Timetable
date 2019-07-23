using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoomNaviMobile.Model;

namespace RoomNaviMobile.Services.Subject
{
    public class SubjectService : ISubjectService
    {
        //private string Url = "https://rntwebservice20190404104209.azurewebsites.net/api/subject";
        private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/subject";

        public SubjectService()
        {

        }

        public async Task<ObservableCollection<RoomNaviMobile.Model.Subject>> GetAllSubjectsAsync(string token)
        {
            HttpClient client = new HttpClient();

            string destUrl = Url + "/null";

            var subjects = new ObservableCollection<RoomNaviMobile.Model.Subject>();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl); // Call WebAPI

                string content = result.ToString();

                List<RoomNaviMobile.Model.Subject> results = JsonConvert.DeserializeObject<List<RoomNaviMobile.Model.Subject>>(content);

                subjects = new ObservableCollection<RoomNaviMobile.Model.Subject>(results);

                client.Dispose();
            }
            catch (Exception ex)
            {
                string strErr = ex.ToString();
                client.Dispose();

                throw new Exception(strErr);
            }

            return subjects;
        }

        public async Task<ObservableCollection<RoomNaviMobile.Model.Subject>> GetAllSubjectsAsync(string subjectId, string token)
        {
            HttpClient client = new HttpClient();

            string destUrl = Url + "/" + subjectId;

            var subjects = new ObservableCollection<RoomNaviMobile.Model.Subject>();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl); // Call WebAPI

                string content = result.ToString();

                List<RoomNaviMobile.Model.Subject> results = JsonConvert.DeserializeObject<List<RoomNaviMobile.Model.Subject>>(content);

                subjects = new ObservableCollection<RoomNaviMobile.Model.Subject>(results);

                client.Dispose();
            }
            catch (Exception ex)
            {
                string strErr = ex.ToString();
                client.Dispose();

                throw new Exception(strErr);
            }

            return subjects;
        }


    }
}
