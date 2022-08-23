using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class TcpS
    {
        private TcpListener server;
        private TcpClient client;

        public TcpS(IPAddress ip, int port)
        {
            server = new TcpListener(ip, port);
        }
        public bool Open()
        {
            try
            {
                server.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool GetConnection()
        {
            try
            {
                client = server.AcceptTcpClient();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReadTcp(out string str)
        {
            str = null;
            try
            { 
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                str = reader.ReadLine();
                reader.Close();
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IPAddress GetClientIp()
        {
            return ((IPEndPoint)client.Client.RemoteEndPoint).Address;
        }
        public void Close()
        {
            if (client != null)
            {
                client.Close();
            }
        }
    }
}
