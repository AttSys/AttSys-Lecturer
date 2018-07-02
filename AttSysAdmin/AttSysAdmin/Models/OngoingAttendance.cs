using System;
using System.Collections.Generic;
using System.Text;

namespace AttSysAdmin.Models
{
    public class OngoingAttendance
    {

        public int id { get; set; }
        public string course_name { get; set; }
        public string code { get; set; }
        public int credits { get; set; }
        public string name { get; set; }
        public string venue { get; set; }
        public bool is_active { get; set; }
        public Present_Student[] present_students { get; set; }
    }
}
