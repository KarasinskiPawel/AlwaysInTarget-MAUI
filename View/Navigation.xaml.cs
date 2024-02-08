using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;
using System.Diagnostics;

namespace AlwaysInTarget.View;

public partial class Navigation : ContentPage
{
	public NavigationModel NavigationModel;

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
                new DataRecalculationRequest(Storage.GetStorage().NavigationModel.Altitude
                , Storage.GetStorage().NavigationModel.SelectedSystem), Storage.GetStorage().WeatherConditions
                );

            Storage.GetStorage().NavigationModel.WindStrenght = weatherConditionsSystem.GetWindStrength();
            Storage.GetStorage().NavigationModel.WindDirection = weatherConditionsSystem.GetWindDirection();

            BindingContext = Storage.GetStorage().NavigationModel;
        }
    }

	private void OnCalculateButtonClick(object sender, EventArgs e)
	{

	}

	private void OnAddToPlanButtonClick(object sender, EventArgs e)
	{

	}

	private void OnFlightplanButtonClick(object sender, EventArgs e)
	{

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