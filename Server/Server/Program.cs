using System;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Xml.Serialization;
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
                PrintSM("Please, enter your ip: ", ConsoleColor.White, false);
                errorEnter = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!errorEnter)
                {
                    PrintSM("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter port 
            while (!errorEnter)
            {
                PrintSM("Please, enter tcp port: ", ConsoleColor.White, false);
                errorEnter = int.TryParse(Console.ReadLine(), out port);
                if(!errorEnter)
                {
                    PrintSM("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }

            //start server
            IPEndPoint ipPoint = new IPEndPoint(ip, port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);
                PrintSM("Server start ...", ConsoleColor.Yellow, true);
                while (true)
                {
                    //get connection
                    Socket handler = listenSocket.Accept();
                    //read message
                    ClientWork(handler);
                }
            }
            catch
            {
                PrintSM("Error: Server not started !!!", ConsoleColor.Red, true);
            }

            async void ClientWork(Socket clientSocket)
            {
                PrintSM("New connection ...", ConsoleColor.Cyan, true);
                try
                {
                    string? message;
                    await using (NetworkStream stream = new NetworkStream(clientSocket))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            message = reader.ReadLine();
                        }
                    }
                    DataMess dataMess = new DataMess(DateTime.Now, clientSocket.RemoteEndPoint, message);
                    Console.WriteLine(WriteXML(dataMess));
                }
                catch
                {
                    PrintSM("Error conection !!!", ConsoleColor.Red, true);
                }
                finally
                {
                    if (clientSocket != null)
                    {
                        PrintSM("Connection was broken !!!", ConsoleColor.Blue, true);
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }
                }
            }
        }
        public static void PrintSM(string message, ConsoleColor consoleColor, bool ifNewLine)
        {
            Console.ForegroundColor = consoleColor;
            if (ifNewLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
            Console.ResetColor();
        }
        public static string WriteXML(DataMess dataMess)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataMess));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                {
                    xmlSerializer.Serialize(writer, dataMess);
                    return sww.ToString();
                }
            }
        }
    }
}