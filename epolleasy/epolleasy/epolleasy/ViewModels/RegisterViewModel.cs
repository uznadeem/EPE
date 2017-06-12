using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using epolleasy.Helpers;
using epolleasy.Services;
using Xamarin.Forms;

namespace epolleasy.ViewModels
{
    public class RegisterViewModel : ContentPage
    {
        ApiServices _apiServices = new ApiServices();

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        //public string ImageUrl { get; set; }




        public string Message { get; set; }


        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSuccess = await _apiServices.RegisterAsync(FirstName,LastName,UserName,Email,Password,ConfirmPassword,UserRole,Gender,BirthDate);


                    Settings.Username = UserName;
                    Settings.Password = Password;

                    Message = isSuccess ? "Registered Successfully" : "Retry Later";

                    await DisplayAlert("Alert", Message, "OK");

                });
            }
        }

    }
}
