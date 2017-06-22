using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using epolleasy.Helpers;
using epolleasy.Models;
using epolleasy.Services;
using Xamarin.Forms;

namespace epolleasy.ViewModels
{
    public class AddNewCommunityViewModel : ContentPage
    {


        ApiServices _apiServices = new ApiServices();

        public string CommunityName { get; set; }
        public string CommunityAbout { get; set; }
        public int Privacy { get; set; }
        public string CommunityDomain { get; set; }

        public List<PrivacyLevel> Privacies { get; set; } = new List<PrivacyLevel>()
        {
            new PrivacyLevel(){Name = "Public",Value = 1},
            new PrivacyLevel(){Name = "Private",Value = 2}
        };
        //public PrivacyLevel Privacy { get {...} set {...} } //Implement Notification changes

        public string Message { get; set; }

        public ICommand AddCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var community = new Community
                    {
                        CommunityName = CommunityName,
                        CommunityAbout = CommunityAbout,
                        PrivacyID = Privacy,
                        CommunityDomain = CommunityDomain
                    };
                    var isSuccess = await _apiServices.AddCommunity(community, Settings.AccessToken);

                    Message = isSuccess ? "Community Successfully created" : "Retry Later";

                    await DisplayAlert("Alert", Message, "OK");
                });
            }
        }



    }
}
