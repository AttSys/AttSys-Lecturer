using System;
using System.Collections.Generic;
using System.Text;

namespace AttSysAdmin.Models
{
    public class TeacherData
    {
        public int user { get; set; }
        public Course[] courses { get; set; }
        public Venue[] venues { get; set; }
    }

    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int credits { get; set; }
        public string description { get; set; }
        public Attendance[] attendances { get; set; }
    }

    public class Attendance
    {
        public int id { get; set; }
        public string name { get; set; }
        public string venue { get; set; }
        public bool is_active { get; set; }
        public Present_Student[] present_students { get; set; }
    }

    public class Present_Student
    {
        public string user { get; set; }
        public string full_name { get; set; }
        public string reg_pic { get; set; }
        public string picture { get; set; }
        public string mat_no { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Venue
    {
        public int id { get; set; }
        public string name { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float radius { get; set; }
    }

}

