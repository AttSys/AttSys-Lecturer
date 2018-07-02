using AttSysAdmin.Models;
using AttSysAdmin.Services;
using AttSysAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AttSysAdmin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OngoingAttendanceView : ContentPage, INotifyPropertyChanged
    {
        private static int AttendanceId;
        public OngoingAttendanceView(OngoingAttendance ongoingAttendance)
        {
            InitializeComponent();
            BindingContext = new SingleOngoingAttendanceViewModel(this.Navigation, ongoingAttendance);

            course_name.Text = ongoingAttendance.course_name;
            code.Text = ongoingAttendance.code;
            credits.Text = ongoingAttendance.credits.ToString() + " credits";
            attendance_name.Text = "Attendance Tag: " + ongoingAttendance.name;
            
        }        
    }   
}