using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    /// <summary>
    /// 获取子账号
    /// </summary>
    public class GetSubAccountsParam : BaseRequest
    {
        public GetSubAccountsParam(string userName) : base(userName)
        {
            NamespaceValue = "com.alibaba.account";
            Name = "alibaba.subAccount.list";
            RequestType = "GetSubAccounts";
        }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
        }
    }
}
