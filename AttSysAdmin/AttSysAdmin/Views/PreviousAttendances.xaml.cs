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
	public partial class PreviousAttendances : ContentPage
	{
		public PreviousAttendances (Course course)
		{
			InitializeComponent ();
            var attendances = course.attendances.ToList();
            AttendanceListView.ItemsSource = attendances;
		}

        public async void SingleAttendanceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }


            var attendance = e.SelectedItem as Attendance;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new PresentStudents(attendance));

        }
    }
}