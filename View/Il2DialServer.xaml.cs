using AlwaysInTarget.Calculate;
using AlwaysInTarget.Models;
using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;
using System.Diagnostics;

namespace AlwaysInTarget.View;

public partial class Il2DialServer : ContentPage
{
	public Il2DialServer()
	{
        BindingContext = Storage.GetStorage().Il2DialServerModel;

		InitializeComponent();
	}

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}