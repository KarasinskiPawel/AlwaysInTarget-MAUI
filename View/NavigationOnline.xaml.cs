using AlwaysInTarget.Auxiliary;
using AlwaysInTarget.Calculate;
using AlwaysInTarget.Models;
using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;

namespace AlwaysInTarget.View;

public partial class NavigationOnline : ContentPage
{
    NavigationOnlineModel navigation = new NavigationOnlineModel();

    public NavigationOnline()
	{
		InitializeComponent();

        BindingContext = navigation;

        SetLabels(navigation.SelectedSystem);

        RunRefresh();
    }

    private async void RunRefresh()
    {
        await Task.Run(() => RefershPlaneDataModel());
    }

    private void RefershPlaneDataModel()
    {
        PlaneDataM planeData;
        DataConversion conversion;

        while (true)
        {
            planeData = Storage.GetStorage().UdpClient.GetPlaneDataM();

            if (planeData != null)
            {
                navigation.PlaneType = planeData.PlaneType;
                navigation.Altitude = (int)planeData.Altitude;

                if (navigation.Altitude > 0)
                {
                    WeatherConditionsSystem weatherConditionsSystem = new WeatherConditionsSystem(
                        new DataConversion(
                            navigation.Altitude,
                            navigation.SelectedSystem),
                            Storage.GetStorage().WeatherConditions
                        );

                    navigation.WindStrenght = weatherConditionsSystem.GetWindStrength();
                    navigation.WindDirection = weatherConditionsSystem.GetWindDirection();
                }

                navigation.Course = (int)planeData.Heading;

                navigation.IAS = (int)planeData.Airspeed;

                conversion = new DataConversion((int)planeData.Airspeed, (int)planeData.Altitude, navigation.SelectedSystem);

                navigation.TAS = new IasToTasConversion(conversion.IAS_KM, conversion.Altitude_FT, navigation.SelectedSystem).GetTAS();

                //poprawki
                var output = new AccurateNavigationCalculator(navigation, new DataConversion(navigation.IAS, navigation.Altitude, navigation.SelectedSystem)).Output();

                navigation.WindCorrectionAngel = output.WindCorrectionAngel;
                navigation.TrueHeading = output.Heading;

                navigation.BombSightDeflection = Storage.GetStorage().BombSightModel.BombSightDeflection;
            }

            Thread.Sleep(250);
        }
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private void System_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetLabels(navigation.SelectedSystem);
    }

    private void SetLabels(string selectedSystem)
    {
        try
        {
            switch (selectedSystem)
            {
                case "Metric":
                    lSpeedIas.Text = "IAS (km/h):";
                    lSpeedTas.Text = "TAS (km/h):";

                    lAlt.Text = "Altitude (m)";
                    break;
                case "Imperial":
                    lSpeedIas.Text = "IAS (mph):";
                    lSpeedTas.Text = "TAS (mph):";

                    lAlt.Text = "Altitude (ft)";
                    break;
                default:
                    lSpeedIas.Text = "Error";
                    lSpeedTas.Text = "Error";

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