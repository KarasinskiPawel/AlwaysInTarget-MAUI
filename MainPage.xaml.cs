using AlwaysInTarget.View;
using System.Diagnostics;

namespace AlwaysInTarget
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnWeatherButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Weather());
        }

        private async void OnBombSightButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BombSight());
        }

        private async void OnNavigationButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Navigation());
        }

        private async void OnFlightplanButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FlightPlan());
        }

        private async void OnAboutButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new About());
        }
    }

}
