using AttSysAdmin.Models;
using AttSysAdmin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AttSysAdmin.ViewModels
{
    public class OngoingAttendancesViewModel: INotifyPropertyChanged
    {
        public string token { get; set; }
        public ICommand RefreshCommand { get; set; }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private List<OngoingAttendance> _ongoingAttendancesList;
        public List<OngoingAttendance> OngoingAttendancesList
        {
            get { return _ongoingAttendancesList; }
            set
            {
                _ongoingAttendancesList = value;
                OnPropertyChanged();
            }
        }


        public OngoingAttendancesViewModel()
        {
            RefreshCommand = new Command(RefreshAction);
            token = "Token" + ((App)Application.Current).token;
            OngoingAttendancesList = App.OngoingAttendances;
        }


        public async void RefreshAction()
        {
            IsRefreshing = true;
            var token = ((App)Application.Current).token;
            var APIService = new APIService();
            var isSuccessful = await APIService.FetchTeacherData(token);
            if (isSuccessful)
            {
                OngoingAttendancesList = App.OngoingAttendances;
                IsRefreshing = false;
                return;
            }
            else
            {
                IsRefreshing = false;
                return;
            }
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
