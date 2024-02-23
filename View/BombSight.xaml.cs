namespace AlwaysInTarget.View;

public partial class BombSight : ContentPage
{
	public BombSight()
	{
		InitializeComponent();

        BindingContext = Storage.GetStorage().BombSightModel;
    }

    private void OnCalculateButtonClick(object sender, EventArgs e)
    {

    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}