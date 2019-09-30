using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var udpClient = new Client(args);
        }
    }

    public class Client
    {
        UdpClient _client = new UdpClient();

        public Client(string[] args)
        {
            //Send command line argument over udb forbindelse
            _client.Connect("10.0.0.1", 9000);

            Byte[] sendBytes = Encoding.ASCII.GetBytes(args[0]);

            _client.Send(sendBytes, sendBytes.Length);


            //Receive msg from server
            IPEndPoint server = new IPEndPoint(IPAddress.Any, 9000);
            Byte[] receivedBytes = _client.Receive(ref server);
            string receivedString = Encoding.ASCII.GetString(receivedBytes);

            Console.WriteLine("{0}", receivedString);

        }

    }
}
