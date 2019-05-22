using Model.EmailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Email
{

    /// <summary>
    /// 发送邮件
    /// </summary>
    public static class EmailUtil
    {
        static string QQMailAccount = "";
        static string QQMailAccountAuthorizationCode = "";

        /// <summary>
        /// 发件邮箱账号
        /// </summary>
        static string _QQMmailAccount
        {
            get
            {
                if (string.IsNullOrWhiteSpace(QQMailAccount))
                {
                    QQMailAccount = "";// AukeysADSystem.DAL.SystemConfig.getSystemConfigValue("SupplySendQQMailAccount");
                }
                return QQMailAccount;
            }
        }
        /// <summary>
        /// 邮箱授权码
        /// 私人需要授权码
        /// 企业使用密码
        /// </summary>
        static string _QQMailAccountAuthorizationCode
        {
            get
            {
                if (string.IsNullOrWhiteSpace(QQMailAccountAuthorizationCode))
                {
                    QQMailAccountAuthorizationCode = "";// AukeysADSystem.DAL.SystemConfig.getSystemConfigValue("SupplySendQQMailAccountAuthorizationCode");
                }
                return QQMailAccountAuthorizationCode;
            }
        }
        /// <summary>
        /// 发送qq邮件
        /// </summary>
        /// <param name="toMail">接受邮箱</param>
        /// <param name="subject">标题</param>
        /// <param name="messageContent">内容</param>
        /// <returns></returns>
        public static QQSendMessage SendQQMail(QQMailModel qqMailModel)
        {
            QQSendMessage qqSendMessage = new QQSendMessage() { Content = "", ErrorMessage = "", IsSuccess = true };
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_QQMmailAccount);
                mailMessage.To.Add(new MailAddress(qqMailModel.ReceiveQQMail));
                mailMessage.Subject = qqMailModel.Title;
                mailMessage.Body = qqMailModel.Content;
                mailMessage.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                //   client.Host = "smtp.qq.com";//个人邮箱
                client.Host = "smtp.exmail.qq.com";//企业邮箱
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(_QQMmailAccount, _QQMailAccountAuthorizationCode);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                qqSendMessage.IsSuccess = false;
                qqSendMessage.ErrorMessage = ex.Message;
            }
            return qqSendMessage;
        }

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        /// <param name="qqMailModelList"></param>
        public static List<SupplyMailMessageRespose> SendQQMailList(List<QQMailModel> qqMailModelList)
        {
            List<SupplyMailMessageRespose> qqSendMessages = new List<SupplyMailMessageRespose>();
            QQSendMessage mes = new QQSendMessage();
            foreach (var item in qqMailModelList)
            {
                mes = SendQQMail(item);
                qqSendMessages.Add(new SupplyMailMessageRespose() { Content = mes.Content, ErrorMessage = mes.ErrorMessage, IsReceiveSuccess = mes.IsReceiveSuccess, IsSuccess = mes.IsSuccess });
            }
            return qqSendMessages;
        }
    }
}
