using AttSysAdmin.Models;
using AttSysAdmin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using AttSysAdmin.Views;
using Rg.Plugins.Popup.Extensions;


namespace AttSysAdmin.ViewModels
{
    class NewAttendanceViewModel: INotifyPropertyChanged
    {
        #region Properties
        private INavigation Navigation;
        private Course Course;
        public ICommand NewAttendanceCommand { get; private set; }
        public ICommand RefreshCommand { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();

            }
        }

        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set
            {
                _isError = value;
                OnPropertyChanged();
            }
        }

        private string _loadingStatus;
        public string LoadingStatus
        {
            get { return _loadingStatus; }
            set
            {
                _loadingStatus = value;
                OnPropertyChanged();
            }
        }

        private string _errorStatus;
        public string ErrorStatus
        {
            get { return _errorStatus; }
            set
            {
                _errorStatus = value;
                OnPropertyChanged();
            }
        }

        private bool _retryButtonActive;
        public bool RetryButtonActive
        {
            get { return _retryButtonActive; }
            set
            {
                _retryButtonActive = value;
                OnPropertyChanged();
            }
        }

        private string _attendanceName;
        public string AttendanceName
        {
            get { return _attendanceName; }
            set
            {
                _attendanceName = value;
                OnPropertyChanged();
            }
        }
        
        private string _venue;
        public string Venue
        {
            get { return _venue; }
            set
            {
                _venue = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public NewAttendanceViewModel(INavigation _navigation, Course _course)
        {
            IsBusy = false;
            Navigation = _navigation;
            Course = _course;
            NewAttendanceCommand = new Command(StartNewAttendance);
            RefreshCommand = new Command(RefreshAction);
        }

        private async void StartNewAttendance()
        {
            IsBusy = true;
            LoadingStatus = "Starting Attendance...";
            var token = ((App)Application.Current).token;
            var selectedVenue = App.TeacherData.venues.FirstOrDefault(x => x.name == Venue);
           
            var APIService = new APIService();
            var result = await APIService.StartAttendance(token, AttendanceName, Course.id.ToString() , selectedVenue.id.ToString());
            if (result != null)
            {
                await ReloadTeacherData(token);                
            }
            else
            {
                IsBusy = false;
                ErrorStatus = "An error occurred while attempting to start this Attendance.";
                IsError = true;
            }

        }

        private async Task ReloadTeacherData(string token)
        {
            LoadingStatus = "Refreshing Data...";
            var APIService = new APIService();
            var isSuccessful = await APIService.FetchTeacherData(token);
            if (isSuccessful)
            {

                Application.Current.MainPage = new NavigationPage(new Home());
                await Navigation.PopAllPopupAsync();
            }
            else
            {
                IsBusy = false;
                ErrorStatus = "The Attendance was successfully started. However, an error occurred while refreshing data. Click the Refresh button to retry again, or restart the app.";
                RetryButtonActive = true;
                IsError = true;
            }
        }

        public async void RefreshAction()
        {
            await ReloadTeacherData(((App)Application.Current).token);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
