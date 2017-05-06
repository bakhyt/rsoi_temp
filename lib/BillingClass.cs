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

    [ServiceContract(Name = "BillingREST")]
    public interface IBillingREST
    {
        [OperationContract]
        [WebGet(UriTemplate = BillingRouting.GetAccountRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        int GetAccountID(string userid);
        [OperationContract]
        [WebInvoke(UriTemplate = BillingRouting.IncSumRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        bool IncSum(string userid, int sum);
        [WebInvoke(UriTemplate = BillingRouting.DecSumRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        bool DecSum(string userid, int sum);
    }

    public static class BillingRouting
    {
        public const string GetAccountRoute = "/Accounts/{id}";
        public const string IncSumRoute = "/IncSum";
        public const string DecSumRoute = "/DecSum";
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true,
        UseSynchronizationContext = false)]
    public class BillingService : IBillingREST
    {
        private static Dictionary<string,int> bills = new Dictionary<string, int>();

        public static int INITIALSUM=1000 ;

        public BillingService()
        {
        }

        public int GetAccountID(string userid)
        {
            int v ;
            if (bills.TryGetValue(userid, out v)) return v; else return INITIALSUM;
        }
        public bool IncSum(string userid, int sum)
        {
            int v ;
            if (!bills.TryGetValue(userid, out v)) v = INITIALSUM;
            v += sum;
            bills[userid] = v;
            return true ;
        }
        public bool DecSum(string userid, int sum)
        {
            int v;
            if (!bills.TryGetValue(userid, out v)) v = 0;
            if (v < sum) return false;
            v -= sum;
            bills[userid] = v;
            return true;
        }        
    }
}
