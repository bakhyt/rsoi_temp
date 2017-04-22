using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArendaRESTLib;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace ArendaApp
{
    
    class Program
    {
        static void Main(string[] args)
        {
            ArendaService srv = new ArendaService();
            ServiceHost _serviceHost = new ServiceHost(typeof(ArendaService),new Uri("http://127.0.0.1:3124/ArendaService"));
            _serviceHost.AddServiceEndpoint(typeof(IArendaREST), new BasicHttpBinding(), "");
            _serviceHost.Open();
            Console.WriteLine("Service start");

            Console.ReadLine();
            _serviceHost.Close();
        }
    }
}
