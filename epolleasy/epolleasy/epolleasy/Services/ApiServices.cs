using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://epolleasy.azurewebsites/api/Account/Register", content);

            return response.IsSuccessStatusCode;

        }


        public async Task LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username",userName),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("grant_type","password"),
                
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://epolleasy.azurewebsites.net/Token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);
        }

    }
}
