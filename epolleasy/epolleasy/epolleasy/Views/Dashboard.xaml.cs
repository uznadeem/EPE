using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace epolleasy.Views
{
    public partial class Dashboard : MasterDetailPage
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Detail = new DpActiveForms();

        }

        private void BtnActiveForm_OnClicked(object sender, EventArgs e)
        {
            this.Detail = new DpActiveForms();
            IsPresented = false;
        }

        private void BtnCommunity_OnClicked(object sender, EventArgs e)
        {
            this.Detail = new DpCommunities();
            IsPresented = false;
        }

        private void BtnHistory_OnClicked(object sender, EventArgs e)
        {
            this.Detail = new DpHistory();
            IsPresented = false;
        }

        private void BtnAddCommunity_OnClicked(object sender, EventArgs e)
        {
            this.Detail = new DpCreateCommunity();
            IsPresented = false;
        }

        private void BtnAddForm_OnClicked(object sender, EventArgs e)
        {
            this.Detail = new DpCreateForm();
            IsPresented = false;
        }

        private void BtnAccountSettings_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnProfileSettings_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private async void Logout_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
            IsPresented = false;

        }
    }
}
