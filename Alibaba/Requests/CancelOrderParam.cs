using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public class CancelOrderParam : BaseRequest
    {
        public CancelOrderParam()
        {
            NamespaceValue = "com.alibaba.trade";
            Name = "alibaba.trade.cancel";
            RequestType = "CancelOrder";
        }
        public long tradeID;
        public string cancelReason;
        public string remark;
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            dictParameters.Add("tradeID", tradeID.ToString());
            if (!string.IsNullOrEmpty(cancelReason))
            {
                dictParameters.Add("cancelReason", "buyerCancel");
            }
            if (!string.IsNullOrEmpty(remark))
            {
                dictParameters.Add("remark", remark);
            }
        }
    }
}
