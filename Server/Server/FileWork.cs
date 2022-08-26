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
        public static string WriteXML(DataMess dataMess)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataMess));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented})
                {
                    xmlSerializer.Serialize(writer, dataMess);
                    return sww.ToString();
                }
            }
        }
    } 
}
