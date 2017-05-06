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

namespace BillingApp
{

    class Program
    {
        static void Main(string[] args)
        {
            Config.Load();
            BillingService srv = new BillingService();
            ServiceHost _serviceHost = new ServiceHost(typeof(BillingService), new Uri(Config.ServiceURL));
            _serviceHost.AddServiceEndpoint(typeof(IBillingREST), new BasicHttpBinding(), "");
            _serviceHost.Open();
            Console.WriteLine("Service billing start");

            Console.ReadLine();
            _serviceHost.Close();
        }
    }
}