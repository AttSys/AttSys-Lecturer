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
    class PresentStudentsViewModel: INotifyPropertyChanged
    {
        public string token { get; set; }
        public string AttendanceModelType { get; set; } //Identifies  if the attendance in question is of model type "Attendance" or "OngoingAttendance"
        public Attendance Attendance { get; set; }
        public OngoingAttendance OngoingAttendance { get; set; }
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

        private List<Present_Student> _studentsList;
        public List<Present_Student> StudentsList
        {
            get { return _studentsList; }
            set
            {
                _studentsList = value;
                OnPropertyChanged();
            }
        }


        public PresentStudentsViewModel(Attendance attendance)
        {
            RefreshCommand = new Command(RefreshAction);
            token = "Token" + ((App)Application.Current).token;
            Attendance = attendance;
            StudentsList = Attendance.present_students.ToList();
            AttendanceModelType = "Attendance";
        }

        public PresentStudentsViewModel(OngoingAttendance attendance)
        {
            RefreshCommand = new Command(RefreshAction);
            token = "Token" + ((App)Application.Current).token;
            OngoingAttendance = attendance;
            StudentsList = OngoingAttendance.present_students.ToList();
            AttendanceModelType = "OngoingAttendance";
        }

        public async void RefreshAction()
        {
            IsRefreshing = true;
            var token = ((App)Application.Current).token;
            var APIService = new APIService();
            var isSuccessful = await APIService.FetchTeacherData(token);
            if (isSuccessful)
            {
                if(AttendanceModelType == "Attendance")
                {
                   foreach(var course in App.TeacherData.courses)
                    {
                        var att = course.attendances.FirstOrDefault(x => x.id == Attendance.id);
                        if(att != null)
                        {
                            StudentsList = att.present_students.ToList();
                            IsRefreshing = false;
                            return;
                        }
                    }
                    
                }

                if (AttendanceModelType == "OngoingAttendance")
                {
                    OngoingAttendance = App.OngoingAttendances.FirstOrDefault(x => x.id == OngoingAttendance.id);
                    StudentsList = OngoingAttendance.present_students.ToList();
                }   

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
