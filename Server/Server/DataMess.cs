using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    [XmlRoot("AllData")]
    public class DataMess
    {

        public DateTime datetime { get; set; }
        public string? ip { get; set; }
        public string? message { get; set; }

        public DataMess() { }
        public DataMess(DateTime _datetime, EndPoint _ip, string _message)
        {
            datetime = _datetime;
            if (_ip != null)
            {
                ip = _ip.ToString();
            }
            message = _message;
        }
    }
}
