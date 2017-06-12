using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using epolleasy.Helpers;
using epolleasy.ViewModels;
using epolleasy.Views;
using Xamarin.Forms;

namespace epolleasy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            SetMainPage();
            //MainPage = new NavigationPage(new RegisterPage());
            //MainPage = new CheapIdeas.MainPage();
        }

        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                //if (Settings.AccessTokenExpirationDate < DateTime.UtcNow.AddHours(1))
                //{
                //    var loginViewModel = new LoginViewModel();
                //    loginViewModel.LoginCommand.Execute(null);
                //}
                MainPage = new NavigationPage(new Dashboard());
            }
            else
            //if (!string.IsNullOrEmpty(Settings.Username)
            //         && !string.IsNullOrEmpty(Settings.Password))
            //{
            //    MainPage = new NavigationPage(new LoginPage());
            //}
            //else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
