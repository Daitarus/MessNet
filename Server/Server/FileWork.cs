using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal static class FileWork
    {
        public static bool WriteFile(string fileName, byte[] text)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                {
                    fs.Write(text, 0, text.Length);
                }
                return true;
            }
            catch { return false; }
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
        public static bool WriteXML(string fname, DataMess dataMess)
        {
            XmlDocument xDoc = new XmlDocument();

            try
            {
                xDoc.Load(fname);
                XmlElement? xRoot = xDoc.DocumentElement;
                if (xRoot != null)
                {
                    XmlElement dataElem = xDoc.CreateElement("data");
                    XmlElement dateElem = xDoc.CreateElement("datetime");
                    XmlElement ipElem = xDoc.CreateElement("ip");
                    XmlElement messageElem = xDoc.CreateElement("message");

                    XmlText dateText = xDoc.CreateTextNode(dataMess.datetime.ToString("dd.MM.yyyy HH:mm:ss"));
                    XmlText ipText = xDoc.CreateTextNode(dataMess.ip.ToString());
                    XmlText messageText = xDoc.CreateTextNode(dataMess.message);

                    dateElem.AppendChild(dateText);
                    ipElem.AppendChild(ipText);
                    messageElem.AppendChild(messageText);

                    dataElem.AppendChild(dateElem);
                    dataElem.AppendChild(ipElem);
                    dataElem.AppendChild(messageElem);

                    xRoot?.AppendChild(dataElem);

                    xDoc.Save(fname);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
