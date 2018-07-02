using AttSysAdmin.Models;
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
	public partial class CourseList : ContentPage
	{
		public CourseList ()
		{
			InitializeComponent ();            
            BindingContext = new CourseListViewModel();
		}

        public async void SingleCourseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            
            
            var course = e.SelectedItem as Course;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new SingleCourse(course));
        }
    }
}