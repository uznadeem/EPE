using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using epolleasy.Services;
using Xamarin.Forms;

namespace epolleasy.ViewModels
{
    public class RegisterViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Message { get; set; }


        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isSuccess = await _apiServices.RegisterAsync(Email,Password,ConfirmPassword);

                    Message = isSuccess ? "Registered Successfully" : "Retry Later";
                });
            }
        }

    }
}
