using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ArendaRESTLib;

namespace Portal
{
    class ObjModule
    {
        public static MyHttpServer httpServer;
        public static Thread thread;
        public static IArendaREST channel;
    }
}
