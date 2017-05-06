using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArendaApp
{
    public class Config
    {
        public static string ServiceURL ;
        public static void Load()
        {
            XDocument doc = XDocument.Load("config.xml");
            var data = doc.Element("config").Element("service");
            ServiceURL = data.Element("url").Value;
        }
    }
}
