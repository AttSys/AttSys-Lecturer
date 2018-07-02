using AttSysAdmin.Models;
using AttSysAdmin.Services;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using AttSysAdmin.Views;
using System.Runtime.CompilerServices;

namespace AttSysAdmin.ViewModels
{
    public class SingleOngoingAttendanceViewModel: INotifyPropertyChanged
    {
        public OngoingAttendance Attendance { get; set; }
        public ICommand StopAttendanceCommand { get; set; }
        public ICommand ViewPresentCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public INavigation Navigation { get; set; }

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


        public SingleOngoingAttendanceViewModel(INavigation _navigation, OngoingAttendance _attendance)
        {
            IsBusy = false;
            IsError = false;
            Navigation = _navigation;
            Attendance = _attendance;
            StopAttendanceCommand = new Command(StopAttendance);
            ViewPresentCommand = new Command(ViewPresentStudents);
            RefreshCommand = new Command(RefreshAction);
        }
        public async void StopAttendance()
        {
            IsBusy = true;
            LoadingStatus = "Attempting to Terminate Attendance...";
            var token = ((App)Application.Current).token;
            var APIService = new APIService();
            var isSuccessful = await APIService.StopAttendance(token, Attendance.id.ToString());
            if (isSuccessful)
            {
                await ReloadTeacherData(token);              
            }
            else
            {
                IsBusy = false;
                ErrorStatus = "An error occurred while attempting to stop this attendance.";
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
            }
            else
            {
                IsBusy = false;
                ErrorStatus = "The Attendance was successfully stopped. However, an error occurred while refreshing data. Click the Refresh button to retry again, or restart the app.";
                RetryButtonActive = true;
                IsError = true;
            }
        }

        public async void RefreshAction()
        {
            await ReloadTeacherData(((App)Application.Current).token);
        }

        public async void ViewPresentStudents()
        {
            await Navigation.PushAsync(new PresentStudents(Attendance));
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
