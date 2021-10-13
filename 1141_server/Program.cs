using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _1141_server
{
    class Program
    {
        static List<Client> clients = new List<Client>();

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(
                IPAddress.Parse("192.168.1.11"),
                10000
                );
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(new Client(client));
                Thread thread = new Thread(ListenClient);
                thread.Start(clients.Last());
            }
        }

        static void ListenClient(object p)
        {
            var client = (Client)p;
            while (true)
            {
                string message = client.GetMessage();
                if (message == "exit")
                {
                    clients.Remove(client);
                    client.Close();
                    break;
                }
                clients.ForEach(s=> 
                {
                    if (s != client)
                        s.SendMessage(message);
                });
            }
        }
    }
}
