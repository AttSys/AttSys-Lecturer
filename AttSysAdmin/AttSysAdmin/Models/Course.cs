using System;
using System.Collections.Generic;
using System.Text;

namespace AttSysAdmin.Models
{
    public class Course
    {
        public string name { get; set; }
        public string code { get; set; }
        public int credits { get; set; }
        public string description { get; set; }
        public List<Attendance> attendances { get; set; }
    }
}




//public class Rootobject
//{
//    public int user { get; set; }
//    public Cours[] courses { get; set; }
//    public Venue[] venues { get; set; }
//}

//public class Cours
//{
//    public string name { get; set; }
//    public string code { get; set; }
//    public int credits { get; set; }
//    public string description { get; set; }
//    public Attendance[] attendances { get; set; }
//}

//public class Attendance
//{
//    public int id { get; set; }
//    public string name { get; set; }
//    public string venue { get; set; }
//    public bool is_active { get; set; }
//    public Present_Students[] present_students { get; set; }
//}

//public class Present_Students
//{
//    public int user { get; set; }
//    public string picture { get; set; }
//    public string mat_no { get; set; }
//}

//public class Venue
//{
//    public int id { get; set; }
//    public string name { get; set; }
//    public float latitude { get; set; }
//    public float longitude { get; set; }
//    public float radius { get; set; }
//}
