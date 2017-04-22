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
using System.Xml;
using System.Xml.Linq;

namespace Portal
{
    class Program
    {
        static void Main(string[] args)
        {

            Uri tcpUri = new Uri("http://127.0.0.1:3124/ArendaService");
            EndpointAddress address = new EndpointAddress(tcpUri);
            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IArendaREST> factory = new ChannelFactory<IArendaREST>(binding, address);
            ObjModule.channel = factory.CreateChannel();

            ObjModule.httpServer = new MyHttpServer(8123);
            ObjModule.thread = new Thread(new ThreadStart(ObjModule.httpServer.listen));
            ObjModule.thread.Start();

            Console.WriteLine("Portal started... Press Enter to exit");
            Console.ReadLine();

            ObjModule.httpServer.stop();
            ObjModule.thread.Abort();

            factory.Close();


        }
    }
}
