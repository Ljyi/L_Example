using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using WEB.Models;

namespace WEB.Utils
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public static class SendMailHelper
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
                    QQMailAccount = "";
                }
                return QQMailAccount;
            }
        }
        /// <summary>
        /// 邮箱授权码
        /// </summary>
        static string _QQMailAccountAuthorizationCode
        {
            get
            {
                if (string.IsNullOrWhiteSpace(QQMailAccountAuthorizationCode))
                {
                    QQMailAccountAuthorizationCode = "";
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
                client.Host = "smtp.exmail.qq.com";
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
    }
}