namespace AlwaysInTarget.View;

public partial class About : ContentPage
{
	public About()
	{
		InitializeComponent();
	}

	public async void OnBackButtonClick(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MainPage());
    }
}