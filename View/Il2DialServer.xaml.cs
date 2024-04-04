#nul

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
    TCPServer server;

    public Il2DialServer()
	{
        server = new TCPServer();

        BindingContext = Storage.GetStorage().Il2DialServerModel;

		InitializeComponent();
	}

    private async void OnConnectButtonClick(object sender, EventArgs e)
    {
        await Task.Run(() => server.Run());
    }

    private void OnDisconnectButtonClick(object sender, EventArgs e)
    {
        server.Stop();
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}