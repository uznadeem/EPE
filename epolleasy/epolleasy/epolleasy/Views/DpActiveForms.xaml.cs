using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;

namespace epolleasy.Views
{
    public partial class DpActiveForms : ContentPage
    {
        public DpActiveForms()
        {
            InitializeComponent();
            
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ViewActiveForm());
        }

        private void MenuItem_AddForm(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewForm());
        }
    }
}
