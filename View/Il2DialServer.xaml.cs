#nullable disable

using AlwaysInTarget.Calculate;
using AlwaysInTarget.Models;
using AlwaysInTarget.Network;
using AlwaysInTarget.ViewModels;
using AlwaysInTarget.WindStrengthAndDirection;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Net;

namespace AlwaysInTarget.View;

public partial class Il2DialServer : ContentPage
{
    public Il2DialServer()
	{
        BindingContext = Storage.GetStorage().Il2DialServerModel;
        string ipAddress = string.Empty;

        if (Preferences.Default.ContainsKey("IpAddress"))
            ipAddress = Preferences.Default.Get("IpAddress", "0.0.0.0");

        if (!(ipAddress is null) && ipAddress.Length > 0)
            Storage.GetStorage().Il2DialServerModel.HostIp = ipAddress;

        InitializeComponent();
	}

    private async void OnConnectButtonClick(object sender, EventArgs e)
    {
        await Task.Run(() => Storage.GetStorage().UdpClient.Scan());
        await Task.Run(() => RefreshModel());
    }

    private void RefreshModel()
    {
        while (true)
        {
            Storage.GetStorage().Il2DialServerModel.SetServerStatus(Storage.GetStorage().UdpClient.GetConnectionM());
            Thread.Sleep(1000);
        }
    }

    private void OnDisconnectButtonClick(object sender, EventArgs e)
    {
        Storage.GetStorage().UdpClient.CloseConnection();
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}