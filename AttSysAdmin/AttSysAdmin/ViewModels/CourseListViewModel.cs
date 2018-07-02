using AttSysAdmin.Models;
using AttSysAdmin.Services;
using AttSysAdmin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace AttSysAdmin.ViewModels
{
    public class CourseListViewModel : INotifyPropertyChanged
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

        private List<Course> _coursesList;
        public List<Course> CoursesList
        {
            get { return _coursesList; }
            set
            {
                _coursesList = value;
                OnPropertyChanged();
            }
        }


        public CourseListViewModel()
        {
            RefreshCommand = new Command(RefreshAction);
            token = "Token" + ((App)Application.Current).token;
            CoursesList = App.TeacherData.courses.ToList();

        }

        public async void RefreshAction()
        {
            IsRefreshing = true;
            var token = ((App)Application.Current).token;
            var APIService = new APIService();
            var isSuccessful = await APIService.FetchTeacherData(token);
            if (isSuccessful)
            {
                CoursesList = App.TeacherData.courses.ToList();
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
