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
        public bool GetConnection(out TcpClient client)
        {
            try
            {
                client = server.AcceptTcpClient();
                return true;
            }
            catch
            {
                client = new TcpClient();
                return false;
            }
        }
    }
}
