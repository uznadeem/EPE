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
    public class LoginViewModel
    {
        private ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {

                    await _apiServices.LoginAsync(Username, Password);

                });
            }
        }
    }
}
