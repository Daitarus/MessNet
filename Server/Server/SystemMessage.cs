using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal static class SystemMessage
    {
        private static string[] SysMess = new string[]
        {
            "Please, enter your ip: ",           //0
            "Please, enter tcp port: ",          //1
            "Error: Incorrect data !!!",         //2
            "Server start ...",                  //3
            "Error: Server not started !!!",     //4
            "New connection ...",                //5
            "Error conection !!!",               //6
            "Connection was broken !!!"          //7
        };
        public static void PrintSM(int id, ConsoleColor consoleColor, bool TypePrint)
        {
            Console.ForegroundColor = consoleColor;
            if (TypePrint)
            {
                Console.WriteLine(SysMess[id]);
            }
            else
            {
                Console.Write(SysMess[id]);
            }
            Console.ResetColor();
        }

    }
}
