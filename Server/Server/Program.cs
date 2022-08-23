using System;
using System.Net;

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
            if(tcpS.Open())
            {
                Console.WriteLine("Server start ... ");
                string message;
                while (true)
                {
                    Console.WriteLine("Server ready connect ...");
                    //connection
                    if (tcpS.GetConnection())
                    {
                        ip=tcpS.GetClientIp();
                        //read message
                        Console.WriteLine("New connection ...");
                        if (tcpS.ReadTcp(out message))
                        {
                            //write
                            DataMess dataMess = new DataMess();
                            dataMess.NewDataMess(DateTime.Now, ip, message);
                            Console.WriteLine(FileWork.WriteXML(dataMess));
                            //if(FileWork.WriteXML("Log.xml", dataMess))
                            //{
                            //    Console.WriteLine("Message was write !");
                            //}
                            //else
                            //{
                            //    Console.WriteLine("Error: Message was send, but not write !!!");
                            //}
                        }
                        else
                        {
                            Console.WriteLine("Error: Message don't send, no conection !!!");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error conection !!!");
                        break;
                    }
                }
                tcpS.Close();
            }
            else
            {
                Console.WriteLine("Error: Server not started !!!");
            }
            
            Console.ReadKey();
        }
        //public static DataMess NewDataMess(DateTime dateTime, IPAddress ip, string message)
        //{
        //    DataMess dataMess = new DataMess();
        //    dataMess.dateWrite = dateTime;
        //    dataMess.ip = ip.ToString();
        //    dataMess.message = message;
        //    return dataMess;
        //}
    }
}