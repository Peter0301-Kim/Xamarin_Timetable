using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomNaviMobile.Model
{

    public class Timetable
    {
        public string crn { get; set; }

        public string dayOfWeek { get; set; }
        public string classTime { get; set; }
        public string campus { get; set; }
        public string subjectCode { get; set; }
        public string subjectDesc { get; set; }
        public string classRoom { get; set; }
        public string lecturerId { get; set; }
        public string lecturerName { get; set; }
        public string startTerm { get; set; }
        public string endTerm { get; set; }

        public const string DEF_CRN = "DEF_CRN";
        public const string DEF_DAY_OF_WEEK = "DEF_DAY_OF_WEEK";
        public const string DEF_CLASS_TIME = "DEF_CLASS_TIME";
        public const string DEF_CAMPUS = "DEF_CAMPUS";
        public const string DEF_SUBJECT_CODE = "DEF_SUBJECT_CODE";
        public const string DEF_SUBJECT_DESC = "DEF_SUBJECT_DESC";
        public const string DEF_CLASS_ROOM = "DEF_CLASS_ROOM";
        public const string DEF_LECTURER_ID = "DEF_LECTURER_ID";
        public const string DEF_LECTURER_NAME = "DEF_LECTURER_NAME";
        public const string DEF_START_TERM = "DEF_START_TERM";
        public const string DEF_END_TERM = "DEF_END_TERM";

        public Timetable() : this(DEF_CRN, DEF_DAY_OF_WEEK, DEF_CLASS_TIME, DEF_CAMPUS,
            DEF_SUBJECT_CODE, DEF_SUBJECT_DESC, DEF_CLASS_ROOM, DEF_LECTURER_ID, DEF_LECTURER_NAME, DEF_START_TERM, DEF_END_TERM)
        { }

        public Timetable(string crn, string dayOfWeek, string classTime, string campus, string subjectCode,
                        string subjectDesc, string classRoom, string lecturerId, string lecturerName, string startTerm, string endTerm)
        {
            this.crn = crn;
            this.dayOfWeek = dayOfWeek;
            this.classTime = classTime;
            this.campus = campus;
            this.subjectCode = subjectCode;
            this.subjectDesc = subjectDesc;
            this.classRoom = classRoom;
            this.lecturerId = lecturerId;
            this.lecturerName = lecturerName;
            this.startTerm = startTerm;
            this.endTerm = endTerm;
        }
    }
}
