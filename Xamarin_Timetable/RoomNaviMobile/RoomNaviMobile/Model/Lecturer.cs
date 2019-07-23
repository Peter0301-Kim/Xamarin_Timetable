using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomNaviMobile.Model
{
    public class Lecturer
    {

        public string lecturerId { get; set; }
        public string givenName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }

        public const string DEF_LECTURER_ID = "DEF_SUBJECT_ID";
        public const string DEF_GIVEN_NAME = "DEF_GIVEN_NAME";
        public const string DEF_LAST_NAME = "DEF_LAST_NAME";
        public const string DEF_EMAIL_ADDRESS = "DEF_EMAIL_ADDRESS";
        public Lecturer(string lecturerId, string givenName, string lastName, string emailAddress)
        {
            this.lecturerId = lecturerId;
            this.givenName = givenName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
        }
        public Lecturer() : this(DEF_LECTURER_ID, DEF_GIVEN_NAME, DEF_LAST_NAME, DEF_EMAIL_ADDRESS)
        {

        }
    }
}
