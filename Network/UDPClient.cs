#nullable disable;

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Network
{
    internal class UDPClient
    {
        IPAddress serverIpAddress;
        public UDPClient() 
        {
        
        }

        private void Scan()
        {
            for(int i = 0; i <= 255; i++)
            {
                for(int j = 0; j <= 255; j++)
                {

                }
            }
        }

        private bool UDPScanner(string _serverAddress)
        {
            bool output = false;

            //Use standard constructor, if we use params it will bind the port under the ood. Only the server should bind the port.
            UdpClient client = new UdpClient();

            try
            {
                //Create package to send - content arbitary.
                byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes("Il-2 Client request");

                //Set quick timeouts on scan thread.
                client.Client.ReceiveTimeout = 100;

                //endpoint where server is listening
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_serverAddress), Storage.GetStorage().Il2DialServerModel.Port);

                client.Connect(endPoint);
                client.Send(sendBytes);

                byte[] receivedData = client.Receive(ref endPoint);

                output = true;

                //Server found! Start another thread for the data streamer (with no timeout settings)
                Thread thread = new Thread(() => UDPSender(_serverAddress));
                thread.IsBackground = true;
                thread.Start();
            }
            catch(Exception e)
            {
                Storage.GetStorage().Il2DialServerModel.SetServerStatus(false, e.Message);
                client.Close();
            }

            return output;
        }

        void UDPSender(string _serverAddress)
        {
            //use standard constructor, if we use params it will bind the port under the hood. Only the server should bind the port
            var client = new UdpClient();

            //endpoint where server is listening
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(_serverAddress), portNumber);
            //create a connection - this will hold until closed
            client.Connect(ep);

            //now we have an end point create a listener on a seperate thread
            Thread thread = new Thread(() => UDPListener(client, ep));
            thread.IsBackground = true;
            thread.Start();

            try
            {
                while (true)
                {
                    // Sends a message to the host to which we have connected.
                    byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes("IL-2 Client");
                    client.Send(sendBytes, sendBytes.Length);
                    Thread.Sleep(sendRate);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                client.Close();
            }

            client.Close();
        }
    }
}
