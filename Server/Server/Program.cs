using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool errorEnter=false;
            IPAddress? ip = IPAddress.Parse("127.0.0.1");
            int port = 0;

            //enter ip
            while (!errorEnter)
            {
                SystemMessage.PrintSM(0, ConsoleColor.White, false);
                errorEnter = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!errorEnter)
                {
                    SystemMessage.PrintSM(2, ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter port
            while (!errorEnter)
            {
                SystemMessage.PrintSM(1, ConsoleColor.White, false);
                errorEnter = int.TryParse(Console.ReadLine(), out port);
                if(!errorEnter)
                {
                    SystemMessage.PrintSM(2, ConsoleColor.Red, true);
                }
            }

            //start server
            IPEndPoint ipPoint = new IPEndPoint(ip, port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(1);
                SystemMessage.PrintSM(3, ConsoleColor.Yellow, true);
                while(true)
                {
                    //get connection
                    Socket handler = listenSocket.Accept();
                    //read message
                    ClientWork(handler);
                }
            }
            catch
            {
                SystemMessage.PrintSM(4, ConsoleColor.Red, true);
            }

            async void ClientWork(Socket handler)
            {
                SystemMessage.PrintSM(5, ConsoleColor.Cyan, true);
                try
                {
                    string? message;
                    await using (NetworkStream stream = new NetworkStream(handler))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            message = reader.ReadLine();
                        }
                    }
                    DataMess dataMess = new DataMess(DateTime.Now, handler.RemoteEndPoint, message);
                    Console.WriteLine(FileWork.WriteXML(dataMess));
                }
                catch
                {
                    SystemMessage.PrintSM(6, ConsoleColor.Red, true);
                }
                finally
                {
                    if (handler != null)
                    {
                        SystemMessage.PrintSM(7, ConsoleColor.Blue, true);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
            }
        }

    }
}