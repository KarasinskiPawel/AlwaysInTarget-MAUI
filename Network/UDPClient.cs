#nullable disable

using AlwaysInTarget.Models;
using AlwaysInTarget.TestDataGenerator;
using AlwaysInTarget.ViewModels;

using FluentAssertions;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
//using Windows.Services.Maps;

namespace AlwaysInTarget.Network
{
    public class UDPClient
    {
        private const int sendRate = 16; //
        private bool testPrediction = false;

        private PlaneDataM planeDataM = new PlaneDataM();
        private ConnectionM connectionM = new ConnectionM();

        private bool threadStop = false;

        Thread udpListenerThread;

        public UDPClient() 
        {
        
        }

        public PlaneDataM GetPlaneDataM() => planeDataM;

        public ConnectionM GetConnectionM() => connectionM;

        public void CloseConnection()
        {
            threadStop = true;
            connectionM.SetConnectionStatus(Storage.GetStorage().Il2DialServerModel.HostIp, "Disconnected", false);
        }

        public void Scan()
        {
            threadStop = false;

            string serverAddress = Storage.GetStorage().Il2DialServerModel.HostIp;

            if(serverAddress == "0.0.0.0")
            {
                for (int i = 0; i <= 255; i++)
                {
                    for (int j = 0; j <= 255; j++)
                    {
                        if (!connectionM.Connected)
                        {
                            Storage.GetStorage().Il2DialServerModel.HostIp = $"192.168.{i}.{j}";

                            Thread.Sleep(16);

                            Thread thread = new Thread(() => UDPScanner(Storage.GetStorage().Il2DialServerModel.HostIp));
                            thread.IsBackground = true;
                            thread.Start();
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (connectionM.Connected)
                        break;
                }
            }
            else
            {
                Thread thread = new Thread(() => UDPScanner(Storage.GetStorage().Il2DialServerModel.HostIp));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void UDPScanner(string _serverAddress)
        {
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

                //Server found! Start another thread for the data streamer (with no timeout settings)
                Preferences.Default.Set("IpAddress", _serverAddress);

                Thread thread = new Thread(() => UDPSender(_serverAddress));
                thread.IsBackground = true;
                thread.Start();
            }
            catch(Exception e)
            {
                connectionM.SetConnectionStatus(_serverAddress, e.Message, false);
                client.Close();
            }
        }

        void UDPSender(string _serverAddress)
        {
            //use standard constructor, if we use params it will bind the port under the hood. Only the server should bind the port
            var client = new UdpClient();

            //endpoint where server is listening
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(_serverAddress), Storage.GetStorage().Il2DialServerModel.Port);

            //create a connection - this will hold until closed
            client.Connect(ep);

            //now we have an end point create a listener on a seperate thread
            udpListenerThread = new Thread(() => UDPListener(client, ep));
            udpListenerThread.IsBackground = true;
            udpListenerThread.Start();

            try
            {
                while (!threadStop)
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


        private void UDPListener(UdpClient udpClient, IPEndPoint ep)
        {
            try
            {
                connectionM.SetConnectionStatus(ep.Address.MapToIPv4().ToString(), "Connected", true);

                while (!threadStop)
                {
                    //////blocking call
                    byte[] receivedData = udpClient.Receive(ref ep);

                    ProcessPackage(receivedData);
                }

                connectionM.SetConnectionStatus(ep.Address.MapToIPv4().ToString(), "Disconnected", false);
            }
            catch (Exception e)
            {
                threadStop = true;
                connectionM.SetConnectionStatus(ep.Address.MapToIPv4().ToString(), "Disconnected", false);
            }
        }

        void ProcessPackage(byte[] bytes)
        {
            int p = 0;

            //set length sent from server	
            int floatArrayLength = 38;
            int floatArrayLengthBytes = 4 * floatArrayLength; //4 bytes for float * array length
                                                              //float array
            float[] floats = GetFloats(bytes, p, floatArrayLength);

            //check for Nan, infinity etc and zero them - we can receive garbled data from reading raw memory
            DataCheck(floats);

            if (!testPrediction)
            {
                planeDataM = new PlaneDataM(floats[0], floats[1], floats[2], floats[3], floats[6]);

                //test
                //planeDataM = GeneratorAsync.GetTestData().Result;
            }

            p += floatArrayLengthBytes;

            //version number
            //receiving server version from stream (server -> client)
            planeDataM.ServerVersion = BitConverter.ToSingle(bytes, p);
            p += sizeof(float);

            //plane type string size
            uint stringSize = BitConverter.ToUInt32(bytes, p);
            p += sizeof(uint);

            //plane type string
            string planeType = System.Text.Encoding.UTF8.GetString(bytes, p, (int)stringSize);

            //using setter method so we can check menu status before chaning plane name
            planeDataM.PlaneType = planeType;
        }

        void DataCheck(float[] floats)
        {
            // check for nan infinity etc
            for (int i = 0; i < floats.Length; i++)
            {
                if (float.IsNaN(floats[i]) || float.IsInfinity(floats[i]) || float.IsNegativeInfinity(floats[i]))
                {
                    floats[i] = 0;
                }
            }
        }

        static float[] GetFloats(byte[] bytes, int offset, int floatArrayLength)
        {
            try
            {
                var result = new float[floatArrayLength];
                Buffer.BlockCopy(bytes, offset, result, 0, floatArrayLength * 4); //* 4 for 4byte floats

                return result;

            }
            catch
            {
                return null;
            }

        }
    }
}
