using System;
using System.Collections.Generic;
using System.Text;

namespace LangleyLib.MailServerLib.Models
{

    public class MailAddressCollection
    {
        private Dictionary<string, MailAddress> m_Addresses = new Dictionary<string, MailAddress>();
        internal MailAddressCollection() : base() { }
        /// <summary>增加 Mail</summary>
        /// <param name="mail">多筆 Mail (預設分割符號 ',' ';' )</param>
        public void AddRange(string mail)
        {
            if (!string.IsNullOrEmpty(mail))
                AddRange(mail, new char[] { ';', ',' });
        }
        /// <summary>增加 Mail</summary>
        /// <param name="mail">多筆 Mail (預設)</param>
        /// <param name="separator">分割符號陣列</param>
        public void AddRange(string mail, char[] separator)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                string[] aryMail = mail.Split(separator);
                AddRange(aryMail);
            }
        }

        /// <summary>增加 Mail</summary>
        /// <param name="aryMail">Mail陣列</param>
        public void AddRange(string[] aryMail)
        {
            foreach (var mail in aryMail)
                Add(mail);
        }
        /// <summary>增加 Mail</summary>
        /// <param name="address">Mail</param>
        public void Add(string address)
        {
            Add(address, string.Empty);
        }
        /// <summary>增加 Mail</summary>
        /// <param name="address">Email Address</param>
        /// <param name="displayName">顯示的名稱</param>
        public void Add(string address, string displayName)
        {
            MailAddress find = null;
            string addressTrim = address.Trim();
            if (!m_Addresses.TryGetValue(addressTrim, out find))
            {
                find = new MailAddress { Address = addressTrim, DisplayName = displayName };
                m_Addresses.Add(addressTrim, find);
            }
            else
            {
                m_Addresses[addressTrim].Address = addressTrim;
                m_Addresses[addressTrim].DisplayName = displayName;
            }
        }
        public Dictionary<string, MailAddress>.ValueCollection Addresses
        {
            get { return m_Addresses.Values; }
        }
        public void Clear() { m_Addresses.Clear(); }
    }
}
