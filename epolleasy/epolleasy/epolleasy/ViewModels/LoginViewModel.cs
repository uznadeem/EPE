using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using epolleasy.Helpers;
using epolleasy.Services;
using epolleasy.Views;
using Xamarin.Forms;

namespace epolleasy.ViewModels
{
    public class LoginViewModel : ContentPage
    {
        private ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }

        public string Password { get; set; }



        //public string LoginStatus { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {

                    //var isLogin = await _apiServices.LoginAsync(Username, Password);
                    var accesstoken = await _apiServices.LoginAsync(Username, Password);

                    Settings.AccessToken = accesstoken;

                    //if  (isLogin)
                    //{
                    //    await Navigation.PushModalAsync(new Dashboard());
                    //}

                });
            }
        }

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;

        }
    }
}
