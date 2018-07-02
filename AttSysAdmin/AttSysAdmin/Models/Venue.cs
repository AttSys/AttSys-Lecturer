using System;
using System.Collections.Generic;
using System.Text;

namespace AttSysAdmin.Models
{
    public class Venue
    {
        public int id { get; set; }
        public string name { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int radius { get; set; }
    }
}
