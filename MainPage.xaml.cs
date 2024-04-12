using AlwaysInTarget.Network;
using AlwaysInTarget.View;
using System.Diagnostics;

namespace AlwaysInTarget
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Storage.GetStorage().MainPageModel.Connected = Storage.GetStorage().Il2DialServerModel.Disconnected;

            BindingContext = Storage.GetStorage().MainPageModel;

            InitializeComponent();
        }

        private void AlwaysOnSwitch_Toggled(object sender, ToggledEventArgs e) => DeviceDisplay.Current.KeepScreenOn = true;

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

        private async void OnNavigationOnlineButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationOnline());
        }

        private async void OnFlightplanButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FlightPlan());
        }

        private async void OnDialServerClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Il2DialServer());
        }

        private async void OnAboutButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new About());
        }
    }

}
