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

            //enter ip
            while (!flag)
            {
                Console.Write("Please, enter your ip: ");
                flag = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!flag)
                {
                    Console.WriteLine("Error: Uncorrect ip !!!");
                }
            }
            flag = false;
            //enter port
            while (!flag)
            {
                Console.Write("Please, enter tcp port: ");
                flag = int.TryParse(Console.ReadLine(), out port);
                if (!flag)
                {
                    Console.WriteLine("Error: Uncorrect port !!!");
                }
            }
            flag=false;
            //start client
            TcpClient tcpClient = new TcpClient();
            //connect
            try
            {
                tcpClient.Connect(ip, port);
                Console.WriteLine("Connection successful ...");
                //write message
                string message="";
                while (!flag)
                {
                    Console.WriteLine("Enter your message: ");
                    message = Console.ReadLine();
                    flag = !string.IsNullOrEmpty(message);
                    if(!flag)
                    {
                        Console.WriteLine("Error: Null message !!!");
                    }
                }
                flag = false;
                //send
                try
                {
                    NetworkStream stream = tcpClient.GetStream();
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine(message);                   
                    writer.Close();
                    stream.Close();
                    Console.WriteLine("Message sended !");
                }
                catch
                {
                    Console.WriteLine("Error conection !!!");
                }
            }
            catch
            {
                Console.WriteLine("Error conection !!!");
            }
            Console.ReadKey();
        }
    }
}