using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class TcpC
    {
        private TcpClient client;

        public TcpC(TcpClient _client)
        {
            this.client = _client;
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
