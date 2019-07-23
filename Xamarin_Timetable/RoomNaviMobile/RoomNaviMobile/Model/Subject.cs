using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomNaviMobile.Model
{
    public class Subject
    {
        private string _subjectId;
        private string _subjectName;

        [JsonProperty("SubjectCode")]
        public string subjectId
        {
            get { return _subjectId; }
            set
            {
                _subjectId = value;
            }
        }

        [JsonProperty("SubjectDescription")]
        public string subjectName
        {
            get { return _subjectName; }
            set
            {
                _subjectName = value;
            }
        }


        public const string DEF_SUBJECT_ID = "DEF_SUBJECT_ID";
        public const string DEF_SUBJECT_NAME = "DEF_SUBJECT_NAME";

        public Subject(string subjectId, string subjectName)
        {
            this.subjectId = subjectId;
            this.subjectName = subjectName;
        }
        public Subject() : this(DEF_SUBJECT_ID, DEF_SUBJECT_NAME)
        {

        }



    }
}
