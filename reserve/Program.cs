﻿using ArendaRESTLib;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using System;

namespace ReserveApp
{
    public class Config
    {
        public static string ServiceURL;
        public static void Load()
        {
            XDocument doc = XDocument.Load("config.xml");
            var data = doc.Element("config").Element("service");
            ServiceURL = data.Element("url").Value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Config.Load();
            ReserveService srv = new ReserveService();
            ServiceHost _serviceHost = new ServiceHost(typeof(ReserveService), new Uri(Config.ServiceURL));
            _serviceHost.AddServiceEndpoint(typeof(IReserveREST), new BasicHttpBinding(), "");
            _serviceHost.Open();
            Console.WriteLine("Service reserve start");

            Console.ReadLine();
            _serviceHost.Close();
        }
    }
}
