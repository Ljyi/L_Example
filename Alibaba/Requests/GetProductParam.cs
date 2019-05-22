using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public class GetProductParam : BaseRequest
    {
        public GetProductParam()
        {
            NamespaceValue = "com.alibaba.product";
            Name = "alibaba.cross.productInfo";
            RequestType = "GetProduct";
        }
        public long productId { get; set; }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            dictParameters.Add("productId", productId.ToString());
        }
    }
}
