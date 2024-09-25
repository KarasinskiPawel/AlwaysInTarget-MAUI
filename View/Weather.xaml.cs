using AlwaysInTarget.Models;
using AlwaysInTarget.ViewModels;
using System;

namespace AlwaysInTarget.View;

public partial class Weather : ContentPage
{
    WeatherModel weatherModel = new WeatherModel(Storage.GetStorage().WeatherConditions);
	public Weather()
    {
        InitializeComponent();

        NavigationPage.SetHasBackButton(this, false);

        BindingContext = weatherModel;
	}

    private async void OnAddClick(object sender, EventArgs e)
    {
        if (!weatherModel.Add())
        {
            await DisplayAlert("Weather", "The maximum number of rows has been reached.", "OK");
        }
    }

    private async void OnRemoveClick(object sender, EventArgs e)
    {
        if (!weatherModel.Remove())
        {
            await DisplayAlert("Weather", "It is not possible to delete all rows.", "OK");
        }
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}