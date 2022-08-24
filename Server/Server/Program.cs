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
                Console.Write("Please, enter your ip: ");
                flag = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!flag)
                {
                    Console.WriteLine("Error: Incorrect ip !!!");
                }
            }
            flag = false;

            //enter port
            while (!flag)
            {
                Console.Write("Please, enter tcp port: ");
                flag = int.TryParse(Console.ReadLine(), out port);
                if(!flag)
                {
                    Console.WriteLine("Error: Uncorrect port !!!");
                }
            }

            //start server
            TcpS tcpS = new TcpS(ip, port);
            string message;
            if (tcpS.Open())
            {
                Console.WriteLine("Server start ... ");
                while (true)
                {
                    TcpClient client = new TcpClient();                  
                    //connection
                    if (tcpS.GetConnection(out client))
                    {
                        //thread for new client
                        Thread clientThread = new Thread(() => ClientWork(client));
                        clientThread.Start();
                    }
                    else
                    {
                        Console.WriteLine("Error conection !!!");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: Server not started !!!");
            }
            Console.ReadKey();

            void ClientWork(TcpClient client)
            {
                string message;
                TcpC tcpC = new TcpC(client);
                IPAddress ip = tcpC.GetClientIp();
                //read message
                Console.WriteLine("New connection ...");
                if (tcpC.ReadTcp(out message))
                {
                    //write
                    DataMess dataMess = new DataMess(DateTime.Now, ip, message);
                    Console.WriteLine(FileWork.WriteXML(dataMess));
                }
                else
                {
                    Console.WriteLine("Error: Message don't send, no conection !!!");
                }
                tcpC.Close();
            }
        }

    }
}