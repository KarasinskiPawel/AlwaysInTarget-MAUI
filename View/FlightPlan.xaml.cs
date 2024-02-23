namespace AlwaysInTarget.View;

public partial class FlightPlan : ContentPage
{
	public FlightPlan()
	{
		InitializeComponent();

        BindingContext = Storage.GetStorage().FlightPlanModel;
    }

	public async void OnNavigationButtonClick(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new Navigation());
    }

    public void OnRemoveAllClick(object sender, EventArgs e)
    {
        Storage.GetStorage().FlightPlanModel.Clear();
    }

    public async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}