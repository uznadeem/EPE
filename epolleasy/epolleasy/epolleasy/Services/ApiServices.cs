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
using Newtonsoft.Json.Linq;

namespace epolleasy.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterAsync(String firstName, String lastName, String username, String email, String password, String confirmPassword, String userRole, String gender, DateTime birthDate)
        {

            var client = new HttpClient();

            var model = new RegisterBindingModel
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = username,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword,
                UserRole = userRole,
                Gender = gender,
                BirthDate = birthDate,

            };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://epolleasy.azurewebsites.net/api/Account/Register", content);

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

            var jwt = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);

            var accessToken = jwtDynamic.Value<string>("access_token"); //variable for acsess_token

            Debug.WriteLine(jwt);

            //if (!string.IsNullOrEmpty(accessToken))
            //{
            //    return response.IsSuccessStatusCode;
            //}
            //else
            //{
            //    return response.IsSuccessStatusCode;
            //}


        }



        public async Task<Dashboard> GetDashboard(string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);

            var json = await client.GetStringAsync("http://epolleasy.azurewebsites.net/api/AdminApi");

            var myDashboard = JsonConvert.DeserializeObject<Dashboard>(json);

            return myDashboard;

        }

    }
}
