using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EmailModel
{
    /// <summary>
    /// 发送QQ邮件Model
    /// </summary>
    public class QQMailModel
    {
        /// <summary>
        /// 接收邮箱
        /// </summary>
        public string ReceiveQQMail { get; set; }
        /// <summary>
        /// 采购员邮箱（抄送）
        /// </summary>
        public string BuyerEmail { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// QQ邮件请求
    /// </summary>
    public class QQSendMessage
    {
        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否接收成功（客户端）
        /// </summary>
        public bool IsReceiveSuccess { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    public class SupplyMailMessageRespose : QQSendMessage
    {

    }
}
