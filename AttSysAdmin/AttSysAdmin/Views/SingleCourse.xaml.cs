using AttSysAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AttSysAdmin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SingleCourse : ContentPage
	{
        public Models.Attendance activeAttendance { get; set; }
        public Course course { get; set; }
        public SingleCourse (Course _course)
		{
			InitializeComponent ();
            course = _course;
            name.Text = _course.name;
            code.Text = _course.code;
            credits.Text = _course.credits + " Credits";
            description.Text = _course.description;

            activeAttendance = _course.attendances.FirstOrDefault(x => x.is_active == true);
            if (activeAttendance == null)
            {
                StartOrStop.Text = "Start a New Attendance";
                StartOrStop.BackgroundColor = Color.FromHex("#d82d97");
                StartOrStop.Clicked += StartNewClicked;
            }
            else
            {
                StartOrStop.Text = "View Ongoing Attendance";
                StartOrStop.BackgroundColor = Color.FromHex("#c9082b");
                StartOrStop.Clicked += ViewOnGoingClicked;
            }
		}

        private void ViewOnGoingClicked(object sender, EventArgs e)
        {
            var ongoingAtt = App.OngoingAttendances.FirstOrDefault(x => x.id == activeAttendance.id);
            Navigation.PushAsync(new OngoingAttendanceView(ongoingAtt));
        }

        private async void StartNewClicked(object sender, EventArgs e)
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new NewAttendancePartial(course), true);
        }

        private async void ViewPreviousClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreviousAttendances(course));
        }

    }
}