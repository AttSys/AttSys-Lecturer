using AttSysAdmin.Models;
using AttSysAdmin.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AttSysAdmin.Services;

namespace AttSysAdmin.ViewModels
{

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties
        private INavigation Navigation;
        public ICommand LoginCommand { get; private set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

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
        #endregion

        public LoginViewModel(INavigation _navigation)
        {
            IsBusy = false;
            IsError = false;
            Navigation = _navigation;
            LoginCommand = new Command(LoginAction);
        }


        public async void LoginAction()
        {
            IsBusy = true;
            LoadingStatus = "Signing In...";
            
            var APIService = new APIService();
           var token = await APIService.Login(Username, Password);
            if(token == "error")
            {
                IsBusy = false;
                ErrorStatus = "An error occurred while signing in";
                IsError = true;
                return;
            }

            else
            {
                await GetTeacherData(token);
            }           
        }

        public async Task GetTeacherData(string token)
        {
            LoadingStatus = "Fetching Data...";
            var APIService = new APIService();
            var isSuccessful = await APIService.FetchTeacherData(token);
            if (isSuccessful)
            {
                Application.Current.MainPage = new NavigationPage(new Home());
            }
            else
            {
                IsBusy = false;
                ErrorStatus = "An error occurred while fetching Lecturer data";
                IsError = true;
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
