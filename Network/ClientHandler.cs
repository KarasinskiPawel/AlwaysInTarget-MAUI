using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Network
{
    class ClientHandler
    {
        private TcpClient client;

        public ClientHandler(TcpClient client)
        {
            this.client = client;
        }

        public void StartHandling()
        {
            // Utwórz strumień sieciowy dla klienta
            NetworkStream stream = client.GetStream();

            // Odbieranie danych od klienta
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                // Konwertuj otrzymane dane na string
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Odebrano: {dataReceived}");

                // Odpowiedź do klienta (echo)
                byte[] response = Encoding.ASCII.GetBytes($"Potwierdzam otrzymanie: {dataReceived}");
                stream.Write(response, 0, response.Length);
            }

            // Zamknij połączenie z klientem
            client.Close();
        }
    }
}
