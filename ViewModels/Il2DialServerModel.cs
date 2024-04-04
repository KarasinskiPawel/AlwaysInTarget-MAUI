#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.ViewModels
{
    public class Il2DialServerModel : INotifyPropertyChanged
    {
        private string _iPAddres;
        private int _port;
        private string _masterServerIP;
        private bool _connected;
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
        public string MasterServerIP {
            get => _masterServerIP;
            set { 
                _masterServerIP = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MasterServerIP)));
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
            MasterServerIP = "0.0.0.0";
            Connected = false;
        }

        public void SetServerStatus(bool connected, string message)
        {
            Connected = connected;
            ServerStatus = message;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
