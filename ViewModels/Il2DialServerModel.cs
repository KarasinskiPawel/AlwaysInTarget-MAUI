#nullable disable

using AlwaysInTarget.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    public class Il2DialServerModel : INotifyPropertyChanged
    {
        private string _iPAddres;
        private int _port;
        private string _hostIp;
        private bool _connected;
        private bool _disconnected;
        private string _serverStatus;

        public string IPAddres {
            get => _iPAddres;
            set {
                _iPAddres = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IPAddres)));
            }
        }
        public int Port {
            get => _port;
            set {
                _port = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Port)));
            }
        }
        public string HostIp {
            get => _hostIp;
            set {
                _hostIp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HostIp)));
            }
        }

        public bool Connected
        {
            get => _connected;
            set
            {
                _connected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Connected)));
            }
        }

        public bool Disconnected
        {
            get => _disconnected;
            set
            {
                _disconnected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Disconnected)));
            }
        }

        public string ServerStatus { get => _serverStatus;
            set
            {
                _serverStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ServerStatus)));
            }
        }

        public Il2DialServerModel()
        {
            IPAddres = "127.0.0.1";
            Port = 11200;
            HostIp = "0.0.0.0";
            Connected = true;
            Disconnected = false;
        }
        public void SetServerStatus(ConnectionM connectionM)
        {
            Connected = !connectionM.Connected;
            Disconnected = connectionM.Connected;
            ServerStatus = connectionM.ConnectionStatus;
            HostIp = connectionM.HostIp;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
