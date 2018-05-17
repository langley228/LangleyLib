using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using LangleyLib.MailServerLib.Configuration;

namespace LangleyLib.MailServerLib
{
    /// <summary>
    /// SmtpSender
    /// </summary>
    public class SmtpSender : IDisposable
    {
        private Models.AttachmentCollection m_Attachments = new Models.AttachmentCollection();
        private Models.MailAddressCollection m_To = new Models.MailAddressCollection();
        private Models.MailAddressCollection m_Cc = new Models.MailAddressCollection();
        private Models.MailAddressCollection m_Bcc = new Models.MailAddressCollection();
        private string m_Body;
        private string m_Subject;
        private Encoding m_Encoding = Encoding.UTF8;
        private SmtpSetting m_SmtpSetting;
        private bool m_IsBodyHtml = true;
        private MailMessage m_MailMessage = null;
        private SmtpClient m_Client = null;

        #region Sender 建構子
        /// <summary>
        /// 建構子: SmtpSender
        /// </summary>
        /// <param name="settingkey">Smtp 設定檔 Key</param>
        public SmtpSender(string settingkey)
            : this(ConfigManager.GetConfig().GetSmtpSetting(settingkey))
        {
        }
        /// <summary>
        /// 建構子: SmtpSender
        /// </summary>
        /// <param name="setting">Smtp 設定檔</param>
        public SmtpSender(SmtpSetting setting)
        {
            m_SmtpSetting = setting;
        }
        #endregion

        #region 屬性
        /// <summary>Server設定</summary>
        public SmtpSetting SmtpSetting
        {
            get { return m_SmtpSetting; }
            set { m_SmtpSetting = value; }
        }
        /// <summary>收信人清單</summary>
        public Models.MailAddressCollection To
        {
            get { return m_To; }
        }

        /// <summary>副本清單</summary>
        public Models.MailAddressCollection Cc
        {
            get { return m_Cc; }
        }

        /// <summary>密件副本清單</summary>
        public Models.MailAddressCollection Bcc
        {
            get { return m_Bcc; }
        }

        /// <summary>主旨</summary>
        public string Subject
        {
            get { return m_Subject; }
            set { m_Subject = value; }
        }

        /// <summary>內容</summary>
        public string Body
        {
            get { return m_Body; }
            set { m_Body = value; }
        }

        /// <summary>編碼</summary>
        public Encoding Encoding
        {
            get { return m_Encoding; }
            set { m_Encoding = value; }
        }

        /// <summary>是否為Html格式</summary>
        public bool IsBodyHtml
        {
            get { return m_IsBodyHtml; }
            set { m_IsBodyHtml = value; }
        }

        /// <summary>夾檔</summary>
        public Models.AttachmentCollection Attachments
        {
            get { return m_Attachments; }
        }


        #endregion

        #region 收件、CC、夾檔相關方法



        /// <summary>
        /// 檢核email格式
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public bool IsValid(string emailAddress)
        {
            try
            {
                //只作為檢核用 
                MailAddress m = new MailAddress(emailAddress, emailAddress);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 清空 收件 CC 夾檔
        /// </summary>
        public void Clear()
        {
            this.m_Attachments.Clear();
            this.m_To.Clear();
            this.m_Cc.Clear();
        }


        #endregion

        #region 寄信方法
        /// <summary>
        /// 寄信方法
        /// </summary>
        public void Send()
        {
            Send(this.m_SmtpSetting, this.m_Subject, this.m_Body, this.m_Encoding, this.m_IsBodyHtml);
        }
        /// <summary>
        /// 寄信方法
        /// </summary>
        /// <param name="smtpSetting">Server設定</param>
        /// <returns></returns>
        public void Send(SmtpSetting smtpSetting)
        {
            Send(smtpSetting, this.m_Subject, this.m_Body, this.m_Encoding, this.m_IsBodyHtml);
        }
        /// <summary>
        /// 寄信方法
        /// </summary>
        /// <param name="mailTos">收信人Email Address</param>
        /// <param name="mailCcs">副本Email Address</param>
        /// <param name="subject">主旨</param>
        /// <param name="body">內文</param>
        /// <returns></returns>
        public void Send(string subject, string body)
        {
            Send(this.m_SmtpSetting, subject, body, this.m_Encoding, this.m_IsBodyHtml);
        }
        /// <summary>
        /// 寄信方法
        /// </summary>
        /// <param name="smtpSetting">Server設定</param>
        /// <param name="mailTos">收信人Email Address</param>
        /// <param name="mailCcs">副本Email Address</param>
        /// <param name="subject">主旨</param>
        /// <param name="body">內文</param>
        /// <returns></returns>
        public void Send(SmtpSetting smtpSetting, string subject, string body)
        {
            Send(smtpSetting, subject, body, this.m_Encoding, this.m_IsBodyHtml);
        }
        /// <summary>
        /// 寄信方法
        /// </summary>
        /// <param name="mailTos">收信人Email Address</param>
        /// <param name="mailCcs">副本Email Address</param>
        /// <param name="subject">主旨</param>
        /// <param name="body">內文</param>
        /// <param name="encoding">編碼</param>
        /// <param name="isBodyHtml">是否為Html格式</param>
        /// <param name="files">要夾帶的附檔</param>
        /// <returns>回傳寄信是否成功(true:成功,false:失敗)</returns>
        public void Send(string subject, string body, Encoding encoding, bool isBodyHtml)
        {
            Send(this.m_SmtpSetting, subject, body, encoding, isBodyHtml);
        }
        /// <summary>
        /// 寄信方法
        /// </summary>
        /// <param name="smtpSetting">Server設定</param>
        /// <param name="mailTos">收信人Email Address</param>
        /// <param name="mailCcs">副本Email Address</param>
        /// <param name="subject">主旨</param>
        /// <param name="body">內文</param>
        /// <param name="encoding">編碼</param>
        /// <param name="isBodyHtml">是否為Html格式</param>
        /// <param name="files">要夾帶的附檔</param>
        /// <returns>回傳寄信是否成功(true:成功,false:失敗)</returns>
        public void Send(SmtpSetting smtpSetting, string subject, string body, Encoding encoding, bool isBodyHtml)
        {
            //建立MailMessage物件
            m_MailMessage = new MailMessage();
            m_MailMessage.BodyEncoding = encoding;
            m_MailMessage.SubjectEncoding = encoding;

            //指定一位寄信人MailAddress
            m_MailMessage.From = new MailAddress(smtpSetting.Address, smtpSetting.DisplayName);
            //信件主旨
            m_MailMessage.Subject = subject;
            //信件內容
            m_MailMessage.Body = body;
            //信件內容 是否採用Html格式
            m_MailMessage.IsBodyHtml = isBodyHtml;

            //收件人
            foreach (var address in m_To.Addresses)
            {
                if (!string.IsNullOrEmpty(address.DisplayName))
                    m_MailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                else
                    m_MailMessage.To.Add(new MailAddress(address.Address));
            }

            //Cc      
            foreach (var address in m_Cc.Addresses)
            {
                if (!string.IsNullOrEmpty(address.DisplayName))
                    m_MailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                else
                    m_MailMessage.CC.Add(new MailAddress(address.Address));
            }

            //Bcc 
            foreach (var address in m_Bcc.Addresses)
            {
                if (!string.IsNullOrEmpty(address.DisplayName))
                    m_MailMessage.Bcc.Add(new MailAddress(address.Address, address.DisplayName));
                else
                    m_MailMessage.Bcc.Add(new MailAddress(address.Address));
            }

            //附件處理    
            foreach (var attachment in m_Attachments.Values)
            {
                Attachment attfile = new Attachment(attachment.FileName);
                if (!string.IsNullOrEmpty(attachment.DisplayName))
                    attfile.Name = attachment.DisplayName;
                else
                    attfile.Name = Path.GetFileName(attachment.FileName);
                m_MailMessage.Attachments.Add(attfile);
            }

            m_Client = new SmtpClient(smtpSetting.Host, smtpSetting.Port);

            if (!string.IsNullOrEmpty(smtpSetting.UserName) && !string.IsNullOrEmpty(smtpSetting.Password))
                m_Client.Credentials = new NetworkCredential(smtpSetting.UserName, smtpSetting.Password);//寄信帳密

            m_Client.EnableSsl = smtpSetting.EnableSsl;
            m_Client.Send(m_MailMessage);//寄出一封信
        }

        #endregion

        /// <summary>
        /// 釋放 資源
        /// </summary>
        public void Dispose()
        {
            this.Clear();
            //釋放每個附件，才不會Lock住
            if (m_MailMessage != null && m_MailMessage.Attachments != null && m_MailMessage.Attachments.Count > 0)
            {
                for (int i = 0; i < m_MailMessage.Attachments.Count; i++)
                {
                    m_MailMessage.Attachments[i].ContentStream.Close();
                    m_MailMessage.Attachments[i].ContentStream.Dispose();
                    m_MailMessage.Attachments[i].Dispose();
                }
            }
            if (m_MailMessage != null)
                m_MailMessage.Dispose();
            if (m_Client != null)
                m_Client.Dispose();
        }
    }


}
