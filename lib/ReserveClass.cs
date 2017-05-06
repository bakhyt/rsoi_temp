using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Linq;

namespace ArendaRESTLib
{

    [ServiceContract(Name = "ReserveREST")]
    public interface IReserveREST
    {
        [OperationContract]
        [WebGet(UriTemplate = ReserveRouting.IsRoomReserved, BodyStyle = WebMessageBodyStyle.Bare)]
        bool IsRoomReserved(string roomid);
        [OperationContract]
        [WebInvoke(UriTemplate = ReserveRouting.DoReserveRoom, BodyStyle = WebMessageBodyStyle.Bare)]
        bool DoReserveRoom(string roomid);
        
    }

    public static class ReserveRouting
    {
        public const string IsRoomReserved = "/Reserved/{id}";
        public const string DoReserveRoom = "/DoReserve";
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true,
        UseSynchronizationContext = false)]
    public class ReserveService : IReserveREST
    {
        private static List<String> res = new List<String>();

        public ReserveService()
        {
        }

        public bool IsRoomReserved(string roomid)
        {
            return res.Contains(roomid);
        }

        public bool DoReserveRoom(string roomid)
        {
            if (!IsRoomReserved(roomid)) res.Add(roomid);
            return true;
        }
    }
}
