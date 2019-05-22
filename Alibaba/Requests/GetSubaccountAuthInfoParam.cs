using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Alibaba.Requests
{
    public class GetSubaccountAuthInfoParam : BaseRequest
    {
        public GetSubaccountAuthInfoParam()
        {
            NamespaceValue = "system.oauth2";
            Name = "subaccount.auth.list";
            RequestType = "SubaccountAuthInfo";
        }
        public List<string> SubUserIdentityList { get; set; }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            if (SubUserIdentityList != null && SubUserIdentityList.Count > 0)
            {
                //string subUserIdentity = "[" + string.Join(",", SubUserIdentityList) + "]";
                //string encodedUrl = HttpUtility.UrlEncode(subUserIdentity).ToUpper();
                ////subUserIdentity = "%5B%22%E6%B7%B1%E5%9C%B3%E5%82%B2%E5%9F%BA2018%3Axs057%22%5D";
                //dictParameters.Add("subUserIdentityList", encodedUrl);

                //   dictParameters.Add("subUserIdentityList", "[" + string.Join(",", SubUserIdentityList) + "]");
                dictParameters.Add("subUserIdentityList", JsonConvert.SerializeObject(SubUserIdentityList));
            }
        }
    }
}
