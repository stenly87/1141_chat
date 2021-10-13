using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace _1141_chat
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new 
                TcpClient("192.168.1.11", 10000);
            Thread thread = new Thread(ListenServer);
            thread.Start(tcpClient);
            BinaryWriter writer = new BinaryWriter(
                tcpClient.GetStream());
            while (true)
            {
                string message = Console.ReadLine();
                writer.Write(message);
                if (message == "exit")
                    break;
            }
        }

        static void ListenServer(object p)
        {
            BinaryReader reader = new BinaryReader((
                (TcpClient)p).GetStream());
            while (true)
            {
                string message = reader.ReadString();
                if (message == "exit")
                    break;
                Console.WriteLine(message);
            }
        }
    }
}
