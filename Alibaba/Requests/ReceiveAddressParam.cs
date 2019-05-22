using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public class ReceiveAddressParam : BaseRequest
    {
        public ReceiveAddressParam()
        {
            NamespaceValue = "com.alibaba.trade";
            Name = "alibaba.trade.receiveAddress.get";
            RequestType = "ReceiveAddress";
            RequestMethod = "Get";
        }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
        }
    }
}
