using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public class SubaccountAuthParam : BaseRequest
    {
        public SubaccountAuthParam()
        {
            NamespaceValue = "system.oauth2";
            Name = "subaccount.auth.add";
            RequestType = "SubaccountAuth";
        }
        /// <summary>
        /// 子账号ID
        /// </summary>
        public List<string> SubUserIdentityList { get; set; }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            if (SubUserIdentityList != null && SubUserIdentityList.Count > 0)
            {
                dictParameters.Add("subUserIdentityList", JsonConvert.SerializeObject(SubUserIdentityList));
                //   dictParameters.Add("subUserIdentityList", "[" + string.Join(",", SubUserIdentityList) + "]");
            }
        }
    }
}
