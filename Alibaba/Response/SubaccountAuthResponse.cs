using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    public class SubaccountAuthResponse
    {
        public string accessToken { get; set; }
        public string aliId { get; set; }
        public string memberId { get; set; }
        public string resourceOwnerId { get; set; }
        public string accessTokenTimeout { get; set; }
    }
    public class SubaccountAuthCancelResponse : BaseResponse
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public bool returnValue { get; set; }
    }
}
