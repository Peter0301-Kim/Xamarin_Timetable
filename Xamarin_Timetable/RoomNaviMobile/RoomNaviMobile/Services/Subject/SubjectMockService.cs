using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNaviMobile.Services.Subject
{
    public class SubjectMockService : ISubjectService
    {
        private ObservableCollection<RoomNaviMobile.Model.Subject> MockSubjects = new ObservableCollection<RoomNaviMobile.Model.Subject>()
        {
            new RoomNaviMobile.Model.Subject { subjectId = "5C#W", subjectName = "C# for Web Development" },
            new RoomNaviMobile.Model.Subject { subjectId = "5DD", subjectName = "Database Design" },
            new RoomNaviMobile.Model.Subject { subjectId = "5EWD", subjectName = "E-Commerce Website Development" },
            new RoomNaviMobile.Model.Subject { subjectId = "5JAW", subjectName = "Java for Web - Basics" },
            new RoomNaviMobile.Model.Subject { subjectId = "5TSD", subjectName = "Team Software Development" }
        };

        public async Task<ObservableCollection<RoomNaviMobile.Model.Subject>> GetAllSubjectsAsync(string sId, string token)
        {
            await Task.Delay(10);

            if (!string.IsNullOrEmpty(token))
            {
                if (sId == "null")
                {
                    return MockSubjects;
                }
                else
                {
                    var tmpS = MockSubjects.Where(s => s.subjectId.Contains(sId));
                    ObservableCollection<RoomNaviMobile.Model.Subject> subjects = new ObservableCollection<RoomNaviMobile.Model.Subject>(tmpS);
                    return subjects;
                }
            }
            else
                return new ObservableCollection <RoomNaviMobile.Model.Subject>();
        }




    }
}
