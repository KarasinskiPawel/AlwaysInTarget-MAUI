using AlwaysInTarget.Models;
using AlwaysInTarget.Storage;
using System;

namespace AlwaysInTarget.View;

public partial class Weather : ContentPage
{
	public List<WeatherConditionM> WeatherConditions { get; set; }
	public Weather()
    {
        InitializeComponent();

		WeatherConditions = WeatherStorage.WeatherConditions;
		BindingContext = this;
	}

    private async void OnConfirmClick(object sender, EventArgs e)
    {
        try
        {
            WeatherStorage.WeatherConditions = WeatherConditions;

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