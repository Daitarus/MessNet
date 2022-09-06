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
            bool errorEnter = false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 0;

            //enter ip
            while (!errorEnter)
            {
                PrintSM("Please, enter server's ip: ", ConsoleColor.White, false);
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
                if (!errorEnter)
                {
                    PrintSM("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;
            IPEndPoint ipPoint = new IPEndPoint(ip, port);
            Socket socket;

            //connect
            while (true)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(ipPoint);
                    PrintSM("Connection successful ...", ConsoleColor.Yellow, true);
                }
                catch
                {
                    PrintSM("Error conection !!!", ConsoleColor.Red, true);
                    break;
                }
                //write message
                string? message = null;
                while (!errorEnter)
                {
                    PrintSM("Enter your message: ", ConsoleColor.Green, false);
                    message = Console.ReadLine();
                    errorEnter = !string.IsNullOrEmpty(message);
                    if (!errorEnter)
                    {
                        PrintSM("Error: Null message !!!", ConsoleColor.Red, true);
                    }
                }
                errorEnter = false;
                //send
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    socket.Send(data);
                    PrintSM("Message sended !", ConsoleColor.Cyan, true);
                }
                catch
                {
                    PrintSM("Error conection !!!", ConsoleColor.Red, true);
                    break;
                }
                if (socket != null)
                {
                    socket.Close();
                }
            }
            Console.ReadKey();
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
    }
}