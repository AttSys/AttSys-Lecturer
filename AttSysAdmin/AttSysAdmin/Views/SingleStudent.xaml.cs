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
	public partial class SingleStudent : ContentPage
	{
		public SingleStudent (Present_Student student)
		{
            InitializeComponent();
            BindingContext = student;
        }
	}
}