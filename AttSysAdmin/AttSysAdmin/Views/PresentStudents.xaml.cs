using AttSysAdmin.Models;
using AttSysAdmin.Services;
using AttSysAdmin.ViewModels;
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
	public partial class PresentStudents : ContentPage
	{
		public PresentStudents (Models.Attendance attendance)
		{
			InitializeComponent ();
            BindingContext = new PresentStudentsViewModel(attendance);

        }

        public PresentStudents(OngoingAttendance attendance)
        {
            InitializeComponent();
            BindingContext = new PresentStudentsViewModel(attendance);
        }

        public async void SingleStudentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }


            var student = e.SelectedItem as Present_Student;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new SingleStudent(student));
        }              
    }
}