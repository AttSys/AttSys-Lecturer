using System.Collections.Generic;

namespace AttSysAdmin.Models
{
    public class Attendance
    {
        public int id { get; set; }
        public string name { get; set; }
        public Venue venue { get; set; }
        public bool is_active { get; set; }
        public List<Student> present_students { get; set; }
    }
}