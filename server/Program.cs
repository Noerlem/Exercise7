using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server udpServer = new Server();
        }
    }
    public class Server
    {
        public Server()
        {
            IPEndPoint IP = new IPEndPoint(IPAddress.Parse("10.0.0.1"), 9000);
            UdpClient server = new UdpClient(IP);

            while (true)
            {
                //Receive data på port 9000
                byte[] receivedBytes = new byte[124];
                IPEndPoint receive = new IPEndPoint(IPAddress.Any, 9000);

                receivedBytes = server.Receive(ref receive);
                string ReceivedString = Encoding.ASCII.GetString(receivedBytes);
                ReceivedString = ReceivedString.ToUpper();

                switch (ReceivedString)
                {
                    case "U":
                        {
                            Console.WriteLine("U was Received");

                            string file = File.ReadAllText(@"/proc/uptime");
                            byte[] sendBytes = Encoding.ASCII.GetBytes(file);
                            server.Send(sendBytes, sendBytes.Length, receive);

                            break;
                        }
                    case "L":
                        {
                            Console.WriteLine("L was Received");

                            string file = File.ReadAllText(@"/proc/loadavg");
                            byte[] sendBytes = Encoding.ASCII.GetBytes(file);
                            server.Send(sendBytes, sendBytes.Length, receive);

                            break;

                        }
                    default:
                        Console.WriteLine("Something else was received: {0}", ReceivedString);
                        break;
                }
            }
        }
    }
}
