using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portal
{
    class Config
    {
        public static string ArendaURL ;
        public static string ReserveURL;
        public static string BillingURL;
        public static int PortalPort;
        public static void Load()
        {
            XDocument doc = XDocument.Load("config.xml");
            foreach(var data in doc.Element("config").Element("services").Elements("service")) {
                if (data.Element("type").Value.Equals("arenda")) ArendaURL = data.Element("url").Value;
                if (data.Element("type").Value.Equals("reserve")) ReserveURL = data.Element("url").Value;
                if (data.Element("type").Value.Equals("billing")) BillingURL = data.Element("url").Value;
            }
            PortalPort = Int32.Parse(doc.Element("config").Element("portal").Element("port").Value) ;
        }
    }
}
