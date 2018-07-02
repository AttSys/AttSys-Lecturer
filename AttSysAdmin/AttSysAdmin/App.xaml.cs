using AttSysAdmin.Models;
using AttSysAdmin.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AttSysAdmin
{
    public partial class App : Application
    {
        public string token { get; set; }
        public static TeacherData TeacherData { get; set; }
        public static List<OngoingAttendance> OngoingAttendances { get; set; }



        public App()
        {
            InitializeComponent();

            MainPage = new Views.Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
