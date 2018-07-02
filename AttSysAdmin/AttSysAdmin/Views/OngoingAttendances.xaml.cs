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
	public partial class OngoingAttendances : ContentPage
	{
		public OngoingAttendances ()
		{
			InitializeComponent ();
            BindingContext = new OngoingAttendancesViewModel();
		}

        public async void SingleAttendanceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var ongoingAttendance = e.SelectedItem as OngoingAttendance;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new OngoingAttendanceView(ongoingAttendance));

        }
    }
}