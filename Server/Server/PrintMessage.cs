using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Server
{
    internal static  class PrintMessage
    {
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
