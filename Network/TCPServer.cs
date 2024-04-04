using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;

namespace AlwaysInTarget.Network
{
    public static class TCPServer
    {
        public static async Task Run()
        {
            IPAddress iPAddress = IPAddress.Parse(Storage.GetStorage().Il2DialServerModel.IPAddres);

            TcpListener tcpListener = new TcpListener(iPAddress, Storage.GetStorage().Il2DialServerModel.Port);

            try
            {
                tcpListener.Start();
                Storage.GetStorage().Il2DialServerModel.ServerStatus = "Server running.";
                System.Threading.Thread.Sleep(1000);

                while (true)
                {
                    Storage.GetStorage().Il2DialServerModel.ServerStatus = "Waiting for calls.";

                    TcpClient client = await tcpListener.AcceptTcpClientAsync();
                    Storage.GetStorage().Il2DialServerModel.ServerStatus = "Connection established.";

                    _ = HandleClientAsync(client);
                }
            }
            catch(Exception e)
            {
                Storage.GetStorage().Il2DialServerModel.ServerStatus = e.Message;
            }
            finally
            {
                tcpListener.Stop();
                Storage.GetStorage().Il2DialServerModel.ServerStatus = "Server stopped.";
            }
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Odebrano: {dataReceived}");

                    byte[] response = Encoding.ASCII.GetBytes($"Potwierdzam otrzymanie: {dataReceived}");
                    await stream.WriteAsync(response, 0, response.Length);
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Storage.GetStorage().Il2DialServerModel.ServerStatus = ex.Message;
            }
        }
    }
}
