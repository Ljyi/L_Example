using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public class SyncProductsPushedParam : BaseRequest
    {
        public SyncProductsPushedParam()
        {
            NamespaceValue = "com.alibaba.product.push";
            Name = "alibaba.cross.syncProductListPushed";
            RequestType = "SyncProductsPushed";
        }
        public List<string> ProductIdList { get; set; }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            if (ProductIdList != null && ProductIdList.Count > 0)
            {
                dictParameters.Add("productIdList", "["+string.Join(",", ProductIdList)+"]");
            }
        }
    }
}
