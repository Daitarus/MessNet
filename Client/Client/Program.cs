using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool flag = false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 0;
            int timeSleep = 1;

            //enter ip
            while (!flag)
            {
                SystemMessage.PrintSM(0, 15, false);
                flag = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!flag)
                {
                    SystemMessage.PrintSM(2, 12, true);
                }
            }
            flag = false;
            //enter port
            while (!flag)
            {
                SystemMessage.PrintSM(1, 15, false);
                flag = int.TryParse(Console.ReadLine(), out port);
                if (!flag)
                {
                    SystemMessage.PrintSM(2, 12, true);
                }
            }
            //enter timeSleep sleep
            while (!flag)
            {
                SystemMessage.PrintSM(8, 15, false);
                flag = int.TryParse(Console.ReadLine(), out timeSleep);
                if (!flag)
                {
                    SystemMessage.PrintSM(2, 12, true);
                }
            }
            flag =false;
            //start client
            TcpClient tcpClient = new TcpClient();
            //connect
            while (true)
            {
                //write message
                string? message = null;
                while (!flag)
                {
                    //SystemMessage.PrintSM(3, 10, false);
                    message = ip.ToString();
                    flag = !string.IsNullOrEmpty(message);
                    if (!flag)
                    {
                        SystemMessage.PrintSM(4, 12, true);
                    }
                }
                flag = false;
                try
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(ip, port);
                    SystemMessage.PrintSM(5, 14, true);
                    //send
                    try
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine(message);
                        writer.Close();
                        stream.Close();
                        SystemMessage.PrintSM(6, 11, true);
                    }
                    catch
                    {
                        SystemMessage.PrintSM(7, 12, true);
                        break;
                    }
                }
                catch
                {
                    SystemMessage.PrintSM(7, 12, true);
                    break;
                }
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
                Thread.Sleep(timeSleep);
            }
            Console.ReadKey();
        }
    }
}