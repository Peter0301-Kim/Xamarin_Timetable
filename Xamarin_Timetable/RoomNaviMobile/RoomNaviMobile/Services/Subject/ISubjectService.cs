using RoomNaviMobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace RoomNaviMobile.Services.Subject
{
    public interface ISubjectService
    {
        Task<ObservableCollection<RoomNaviMobile.Model.Subject>> GetAllSubjectsAsync(string subjectId, string token);
    }
}
