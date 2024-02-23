using AlwaysInTarget.Calculate;
using AlwaysInTarget.Models;
using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;
using System.Diagnostics;

namespace AlwaysInTarget.View;

public partial class Navigation : ContentPage
{
    public Navigation()
	{
		InitializeComponent();

        BindingContext = Storage.GetStorage().NavigationModel;

        SetLabels(Storage.GetStorage().NavigationModel.SelectedSystem);
    }

    private void OnAltitudeChanged(object sender, EventArgs e)
    {
        if(Storage.GetStorage().NavigationModel.Altitude > 0)
        {
            WeatherConditionsSystem weatherConditionsSystem = new WeatherConditionsSystem(
                new DataConversion(Storage.GetStorage().NavigationModel.Altitude
                , Storage.GetStorage().NavigationModel.SelectedSystem), Storage.GetStorage().WeatherConditions
                );

            Storage.GetStorage().NavigationModel.WindStrenght = weatherConditionsSystem.GetWindStrength();
            Storage.GetStorage().NavigationModel.WindDirection = weatherConditionsSystem.GetWindDirection();
        }
    }

	private void OnCalculateButtonClick(object sender, EventArgs e)
	{
        var output = new AccurateNavigationCalculator(Storage.GetStorage().NavigationModel, new DataConversion(Storage.GetStorage().NavigationModel.IAS, Storage.GetStorage().NavigationModel.Altitude, Storage.GetStorage().NavigationModel.SelectedSystem)).Output();

        Storage.GetStorage().NavigationModel.WindCorrectionAngel = output.WindCorrectionAngel;
        Storage.GetStorage().NavigationModel.Heading = output.Heading;

        Storage.GetStorage().NavigationModel.NavigationPointAdded = output.Correct;

    }

	private async void OnAddToPlanButtonClick(object sender, EventArgs e)
	{
        DataConversion conversion = new DataConversion(Storage.GetStorage().NavigationModel.IAS, Storage.GetStorage().NavigationModel.Altitude, Storage.GetStorage().NavigationModel.SelectedSystem);
        NavigationPointM point = new();

        switch (Storage.GetStorage().NavigationModel.SelectedSystem)
        {
            case "Metric":
                point = new(Storage.GetStorage().NavigationModel.Course, conversion.IAS_KM, Storage.GetStorage().NavigationModel.TAS_KM, conversion.Altitude_M, Storage.GetStorage().NavigationModel.WindCorrectionAngel, Storage.GetStorage().NavigationModel.Heading, Storage.GetStorage().BombSightModel.BombSightDeflection); 
                break;
            case "Imperial":
                point = new(Storage.GetStorage().NavigationModel.Course, conversion.IAS_MPH, Storage.GetStorage().NavigationModel.TAS_MPH, conversion.Altitude_FT, Storage.GetStorage().NavigationModel.WindCorrectionAngel, Storage.GetStorage().NavigationModel.Heading, Storage.GetStorage().BombSightModel.BombSightDeflection);
                break;
            default:
                await DisplayAlert("Navigation", "Error - incorrect system.", "OK");
                break;
        }

        if(point is not null)
        {
            if (Storage.GetStorage().FlightPlanModel.Add(point))
            {
                await DisplayAlert("Navigation", "Point added.", "OK");
                Storage.GetStorage().NavigationModel.NavigationPointAdded = false;
            }
            else
            {
                await DisplayAlert("Navigation", "Error", "OK");
            }
        }
	}

	private async void OnFlightplanButtonClick(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new FlightPlan());
    }

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MainPage());
    }

    private void System_SelectedIndexChanged(object sender, EventArgs e)
	{
        SetLabels(Storage.GetStorage().NavigationModel.SelectedSystem);
	}

    private void SetLabels(string selectedSystem)
    {
        try
        {
            switch (selectedSystem)
            {
                case "Metric":
                    lSpeed.Text = "IAS (km/h):";
                    lAlt.Text = "Altitude (m)";
                    break;
                case "Imperial":
                    lSpeed.Text = "IAS (mph):";
                    lAlt.Text = "Altitude (ft)";
                    break;
                default:
                    lSpeed.Text = "Error";
                    lAlt.Text = "Error";
                    break;
            }
        }
        catch
        {
            DisplayAlert("Error", "B³¹d podczas zmiany sytemu.", "OK");
        }
    }
}