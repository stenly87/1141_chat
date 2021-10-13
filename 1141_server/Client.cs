using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _1141_server
{
    public class Client
    {
        TcpClient tcpClient;
        BinaryReader reader;
        BinaryWriter writer;

        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            reader = new BinaryReader(tcpClient.GetStream());
            writer = new BinaryWriter(tcpClient.GetStream());
        }

        internal string GetMessage()
        {
            try
            {
                return reader.ReadString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "exit";
            }
        }

        internal void Close()
        {
            SendMessage("exit");
            reader.Close();
            writer.Close();
        }

        internal void SendMessage(string message)
        {
            try
            {
                writer.Write(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
