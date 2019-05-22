using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    /// <summary>
    /// 子账号授权信息
    /// </summary>
    public class SubaccountAuthInfoResponse
    {
        public SubaccountAuthResult result { get; set; }
    }
    public class SubaccountAuthResult : BaseResponse
    {
        public List<SubaccountAuthReturnValue> returnValue { get; set; }
    }
    public class SubaccountAuthReturnValue
    {
        /// <summary>
        /// 授权凭证
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// 主账号loginId
        /// </summary>
        public string adminOwnerId { get; set; }
        /// <summary>
        /// 主账号userId
        /// </summary>
        public string adminUserId { get; set; }
        /// <summary>
        /// appKey
        /// </summary>
        public string clientId { get; set; }
        /// <summary>
        /// appName
        /// </summary>
        public string clientName { get; set; }
        /// <summary>
        /// 授权过期时间
        /// </summary>
        public string gmtExpired { get; set; }
        /// <summary>
        /// 授权用户memberId
        /// </summary>
        public string memberId { get; set; }
        /// <summary>
        /// 授权用户loginId
        /// </summary>
        public string ownerId { get; set; }
        /// <summary>
        /// 资源域
        /// </summary>
        public string resourceScopes { get; set; }
        /// <summary>
        /// 授权站点
        /// </summary>
        public string site { get; set; }
        /// <summary>
        /// 授权状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 是否子账号授权
        /// </summary>
        public bool subAuth { get; set; }
        /// <summary>
        /// 子账号loginId
        /// </summary>
        public string subOwnerId { get; set; }
        /// <summary>
        /// 子账号userId
        /// </summary>
        public long subUserId { get; set; }
        /// <summary>
        /// 授权用户userId
        /// </summary>
        public long userId { get; set; }
    }
}
