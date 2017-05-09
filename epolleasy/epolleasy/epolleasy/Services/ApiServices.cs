using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using epolleasy.Models;
using Newtonsoft.Json;

namespace epolleasy.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {

            var client = new HttpClient();

            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);

            var response = await client.PostAsync("http://epolleasy.azurewebsites.net/api/Account/Register", content);

            return response.IsSuccessStatusCode;

        }
    }
}
