#nullable disable

using AlwaysInTarget.Auxiliary;
using AlwaysInTarget.Calculate;
using AlwaysInTarget.Models;
using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;
using System.Diagnostics;
using System.Numerics;

namespace AlwaysInTarget.View;

public partial class NavigationOnline : ContentPage
{
    NavigationOnlineModel navigation = new NavigationOnlineModel();
    Thread dataRefresh;
    bool dataRefreshStop = false;

    public NavigationOnline()
	{
		InitializeComponent();

        BindingContext = navigation;

        SetLabels(navigation.SelectedSystem);

        RunRefresh();
    }

    private void RunRefresh()
    {
        //await Task.Run(() => RefershPlaneDataModel());
        dataRefresh = new Thread(() => RefershPlaneDataModel());
        dataRefresh.IsBackground = true;
        dataRefresh.Start();
    }

    private void RefershPlaneDataModel()
    {
        PlaneDataM planeData;
        DataConversion conversion;

        while (!dataRefreshStop)
        {
            planeData = Storage.GetStorage().UdpClient.GetPlaneDataM();

            if (planeData != null)
            {
                //dane z Il 2 Dial server zawsze s¹ w systemie metrycznym.
                conversion = new DataConversion((int)planeData.Airspeed_KM, (int)planeData.Altitude_M, "Metric");

                //Rozmieszczenie danuych na wyœwietlaczu.
                navigation.PlaneType = planeData.PlaneType;

                switch (navigation.SelectedSystem)
                {
                    case "Metric":
                        navigation.Altitude = conversion.Altitude_M;
                        navigation.IAS = (int)conversion.IAS_KM;
                        navigation.TAS = new IasToTasConversion(conversion.IAS_KM, conversion.Altitude_FT).GetTAS_KM_H();
                        break;
                    case "Imperial":
                        navigation.Altitude = conversion.Altitude_FT;
                        navigation.IAS = (int)conversion.IAS_MPH;
                        navigation.TAS = new IasToTasConversion(conversion.IAS_KM, conversion.Altitude_FT).GetTAS_MPH();
                        break;
                }

                navigation.Course = planeData.Heading;               

                if (navigation.Altitude > 0)
                {
                    WeatherConditionsSystem weatherConditionsSystem = new WeatherConditionsSystem(conversion, Storage.GetStorage().WeatherConditions);

                    navigation.WindStrenght = weatherConditionsSystem.GetWindStrength();
                    navigation.WindDirection = weatherConditionsSystem.GetWindDirection();
                }

                //poprawki
                var output = new AccurateNavigationCalculator(navigation, conversion).Output();

                navigation.WindCorrectionAngel = output.WindCorrectionAngel;
                navigation.TrueHeading = output.Heading;

                navigation.BombSightDeflection = Storage.GetStorage().BombSightModel.BombSightDeflection;
            }

            Thread.Sleep(17);
        }
    }

    private void Course(float heading)
    {

    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        dataRefreshStop = true;

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