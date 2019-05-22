using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Response
{
    public class SubAccountResponse : BaseResponse
    {
        /// <summary>
        /// 主账号Userid
        /// </summary>
        public string mainUserId { get; set; }
        /// <summary>
        /// 主账号loginId
        /// </summary>
        public string mainLoginId { get; set; }
        /// <summary>
        /// 主账号MemberId
        /// </summary>
        public string mainMemberId { get; set; }
        /// <summary>
        /// 子账号集合
        /// </summary>
        public List<SubAccountList> subAccountList { get; set; }
    }
    public class SubAccountList
    {
        /// <summary>
        /// 子账号登录名
        /// </summary>
        public string loginId { get; set; }
        /// <summary>
        /// 子账号用户ID
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 子账号memberId
        /// </summary>
        public string memberId { get; set; }
    }
}
