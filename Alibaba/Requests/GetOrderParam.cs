using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public class GetOrderParam : BaseRequest
    {
        public GetOrderParam()
        {
            NamespaceValue = "com.alibaba.trade";
            Name = "alibaba.trade.get.buyerView";
            RequestType = "GetOrder";
        }
        public long orderId;
        public string includeFields;
        public string attributeKeys;
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            dictParameters.Add("orderId", orderId.ToString());
            if (!string.IsNullOrEmpty(includeFields))
            {
                dictParameters.Add("includeFields", includeFields.ToString());
            }
            if (!string.IsNullOrEmpty(attributeKeys))
            {
                dictParameters.Add("attributeKeys", attributeKeys.ToString());
            }
        }
        //public void SetOrderParam(long orderId, string webSite, string includeFields, string attributeKeys)
        //{
        //    this.orderId = orderId;
        //    this.webSite = webSite;
        //    this.includeFields = includeFields;
        //    this.attributeKeys = attributeKeys;
        //}
    }
}
