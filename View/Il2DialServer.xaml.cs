#nullable disable

using AlwaysInTarget.Calculate;
using AlwaysInTarget.Models;
using AlwaysInTarget.Network;
using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace AlwaysInTarget.View;

public partial class Il2DialServer : ContentPage
{
    public Il2DialServer()
	{
        BindingContext = Storage.GetStorage().Il2DialServerModel;

		InitializeComponent();
	}

    private async void OnConnectButtonClick(object sender, EventArgs e)
    {
        await Task.Run(() => Storage.GetStorage().UdpClient.Scan());
    }

    private async void OnDisconnectButtonClick(object sender, EventArgs e)
    {
        await Task.Run(() => Storage.GetStorage().UdpClient.Scan());
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}