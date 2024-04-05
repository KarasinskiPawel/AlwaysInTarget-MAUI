#nullable disable

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
        Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

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

        //private string IpRegex(string val)
        //{
        //    string output = string.Empty;

        //    if (string.IsNullOrWhiteSpace(val))
        //    {
        //        MatchCollection result = ip.Matches(val);

        //        if(!(result is null) && result.Count > 0)
        //            output = result[0].ToString();
        //    }

        //    return output;
        //}

        public void SetServerStatus(bool connected, string message)
        {
            Connected = connected;
            ServerStatus = message;
        }

        public void SetServerStatus(bool connected, string message, string masterServerIP)
        {
            Connected = connected;
            ServerStatus = message;
            MasterServerIP= masterServerIP;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
