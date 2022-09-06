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
                PrintMessage.PrintSM("Please, enter your ip: ", ConsoleColor.White, false);
                errorEnter = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!errorEnter)
                {
                    PrintMessage.PrintSM("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter port 
            while (!errorEnter)
            {
                PrintMessage.PrintSM("Please, enter tcp port: ", ConsoleColor.White, false);
                errorEnter = int.TryParse(Console.ReadLine(), out port);
                if(!errorEnter)
                {
                    PrintMessage.PrintSM("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }

            //start server
            IPEndPoint ipPoint = new IPEndPoint(ip, port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket handler;
            try
            {
                PrintMessage.PrintSM("Server start ...", ConsoleColor.Yellow, true);
                while (true)
                {
                    listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    listenSocket.Bind(ipPoint);
                    listenSocket.Listen(1);
                    //get connection
                    handler = listenSocket.Accept();
                    //read message
                    ClientWork(handler);
                    listenSocket.Close();
                }
            }
            catch
            {
                PrintMessage.PrintSM("Error: Server not started !!!", ConsoleColor.Red, true);
            }


            async void ClientWork(Socket clientSocket)
            {
                PrintMessage.PrintSM("New connection ...", ConsoleColor.Cyan, true);
                try
                {
                    string? message = null;
                    await Task.Run(() =>
                    {
                        using (NetworkStream stream = new NetworkStream(clientSocket))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                message = reader.ReadLine();
                            }
                        }
                    });
                    DataMess dataMess = new DataMess(DateTime.Now, clientSocket.RemoteEndPoint, message);
                    Console.WriteLine(PrintMessage.WriteXML(dataMess));
                }
                catch
                {
                    PrintMessage.PrintSM("Error conection !!!", ConsoleColor.Red, true);
                }
                finally
                {
                    if (clientSocket != null)
                    {
                        PrintMessage.PrintSM("Connection was broken !!!", ConsoleColor.Blue, true);
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }
                }
            }
        }      
    }
}