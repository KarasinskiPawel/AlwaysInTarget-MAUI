#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Models
{
    public class ConnectionM
    {
        public string HostIp { get; set; }
        public string ConnectionStatus { get; set; }
        public bool Connected { get; set; }

        public void SetConnectionStatus(string hostIp, string connectionStatus, bool connected)
        {
            HostIp = hostIp;
            ConnectionStatus = connectionStatus;
            Connected = connected;
        }
    }
}
