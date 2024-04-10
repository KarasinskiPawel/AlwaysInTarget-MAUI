namespace AlwaysInTarget.View;

public partial class NavigationOnline : ContentPage
{
	public NavigationOnline()
	{
		InitializeComponent();

        BindingContext = Storage.GetStorage().NavigationModel;

        SetLabels(Storage.GetStorage().NavigationModel.SelectedSystem);
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