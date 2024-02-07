using AlwaysInTarget.ViewModels;

namespace AlwaysInTarget.View;

public partial class Navigation : ContentPage
{
	public NavigationModel NavigationModel;

    public Navigation()
	{
		InitializeComponent();

		NavigationModel = Storage.GetStorage().NavigationModel;
        BindingContext = this;
    }

    private void OnAltitudeChanged(object sender, EventArgs e)
    {

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

	}




}