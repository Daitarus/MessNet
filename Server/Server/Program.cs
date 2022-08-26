using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool flag=false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 0;

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
                if(!flag)
                {
                    SystemMessage.PrintSM(2, 12, true);
                }
            }

            //start server
            TcpS tcpS = new TcpS(ip, port);
            if (tcpS.Open())
            {
                SystemMessage.PrintSM(3, 14, true);
                while (true)
                {
                    TcpClient client = new TcpClient();                  
                    //connection
                    if (tcpS.GetConnection(out client))
                    {
                        //thread for new client
                        SystemMessage.PrintSM(5, 11, true);
                        Thread clientThread = new Thread(() => ClientWork(client));
                        clientThread.Start();
                    }
                    else
                    {
                        SystemMessage.PrintSM(6, 12, true);
                        break;
                    }
                }
            }
            else
            {
                SystemMessage.PrintSM(4, 12, true);
            }
            Console.ReadKey();

            void ClientWork(TcpClient client)
            {
                string message;
                TcpC tcpC = new TcpC(client);
                IPAddress ip = tcpC.GetClientIp();
                //read message
                while (true)
                {
                    if (tcpC.ReadTcp(out message))
                    {
                        //write
                        DataMess dataMess = new DataMess(DateTime.Now, ip, message);
                        Console.WriteLine(FileWork.WriteXML(dataMess));
                    }
                    else
                    {
                        SystemMessage.PrintSM(7, 12, true);
                        break;
                    }
                }
                tcpC.Close();
            }
        }

    }
}