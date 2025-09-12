#nullable disable

using AlwaysInTarget.Graphic;
using AlwaysInTarget.Auxiliary;
using AlwaysInTarget.Calculate;
using AlwaysInTarget.Models;
using AlwaysInTarget.TrueHeading;
using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;

namespace AlwaysInTarget.View;

public partial class CompassOnline : ContentPage
{
    private CompassDrawable _compassDrawable;
    private IDispatcherTimer _refreshTimer;

    NavigationOnlineModel navigation = new NavigationOnlineModel();

    Thread dataRefresh;

    bool dataRefreshStop = false;
    public CompassOnline()
	{
        InitializeComponent();
        BindingContext = navigation;

        _compassDrawable = new CompassDrawable();
        CompassView.Drawable = _compassDrawable;

        StartUdpThread();
        StartFindDirectorRefreshTimer();
    }

    private void StartUdpThread()
    {
        dataRefresh = new Thread(() => RefershPlaneDataModel());
        dataRefresh.IsBackground = true;
        dataRefresh.Start();
    }

    private void StartFindDirectorRefreshTimer()
    {
        _refreshTimer = Dispatcher.CreateTimer();
        _refreshTimer.Interval = TimeSpan.FromMilliseconds(33); // ~30 FPS
        _refreshTimer.Tick += (s, e) =>
        {
            MainThread.BeginInvokeOnMainThread(() => {
                if (CompassView is not null)
                    CompassView.Invalidate();
            });
        };
        _refreshTimer.Start();
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
                var output = new AccurateNavigationCalculator(navigation, conversion, new TrueHeadingOnline()).Output();

                navigation.WindCorrectionAngel = output.WindCorrectionAngel;
                navigation.TrueHeading = output.Heading;

                navigation.GroundSpeed_KM = Math.Round(output.GroundSpeed, 2);
                navigation.Distance_KM += Math.Round(output.Distance, 2);

                navigation.BombSightDeflection = Storage.GetStorage().BombSightModel.BombSightDeflection;

                _compassDrawable.SetCurrentFlightParameters(navigation);
            }

            Thread.Sleep(17);
        }
    }

    public void OnResetButtonClick(object sender, EventArgs e)
    {
        navigation.Distance_KM = 0;
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        dataRefreshStop = true;

        if (dataRefresh != null && dataRefresh.IsAlive)
        {
            dataRefresh.Join(); // blokuje do czasu zakoñczenia w¹tku
        }

        await Navigation.PushAsync(new MainPage());
    }

    private void SwitchMapHdgArrow(object sender, ToggledEventArgs e)
    {

    }
}