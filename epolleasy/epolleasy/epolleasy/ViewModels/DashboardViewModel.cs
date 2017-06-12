using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using epolleasy.Annotations;
using epolleasy.Helpers;
using epolleasy.Models;
using epolleasy.Services;
using Xamarin.Forms;

namespace epolleasy.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {

        ApiServices _apiServices = new ApiServices();
        private Dashboard _userDashboard;

        //public string AccessToken { get; set; }

        public Dashboard UserDashboard
        {
            get
            {
                return _userDashboard;
            }
            set
            {
                _userDashboard = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetDashboard
        {
            get
            {
             return new Command(async () =>
             {
                 var accessToken = Settings.AccessToken;
                 UserDashboard = await _apiServices.GetDashboard(accessToken);
             });
            }

        }


        


        public ICommand LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    Settings.AccessToken = string.Empty;
                    //Debug.WriteLine(Settings.Username);
                    Settings.Username = string.Empty;
                    //Debug.WriteLine(Settings.Password);
                    Settings.Password = string.Empty;

                    // navigate to LoginPage
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
