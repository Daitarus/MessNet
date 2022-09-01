using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class SystemMessage
    {
        private static string[] SysMess = new string[]
        {
            "Please, enter your ip: ",           //0
            "Please, enter tcp port: ",          //1
            "Error: Incorrect data !!!",         //2
            "Enter your message: ",              //3
            "Error: Null message !!!",           //4
            "Connection successful ...",         //5
            "Message sended !",                  //6
            "Error conection !!!",               //7
            "Please, enter time sleep: "         //8
        };
        public static void PrintSM(int id, int numColor, bool TypePrint)
        {
            Console.ForegroundColor = (ConsoleColor)numColor;
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
