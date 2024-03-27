using AlwaysInTarget.Models;
using System;

namespace AlwaysInTarget.View;

public partial class Weather : ContentPage
{
	public List<WeatherConditionM> WeatherConditions { get; set; }
	public Weather()
    {
        InitializeComponent();

        NavigationPage.SetHasBackButton(this, false);

        WeatherConditions = Storage.GetStorage().WeatherConditions;
        BindingContext = this;
	}

    private async void OnConfirmClick(object sender, EventArgs e)
    {
        try
        {
            Storage.GetStorage().WeatherConditions = WeatherConditions;

            await DisplayAlert("Weather", "Data approved.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Weather", ex.Message.ToString(), "OK");
        }
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}