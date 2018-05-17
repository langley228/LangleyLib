using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LangleyLib.MailServerLib.Models
{
    public class AttachmentCollection
    {
        private Dictionary<string, Attachment> m_Attachments = new Dictionary<string, Attachment>();
        internal AttachmentCollection() { }
        /// <summary>
        /// 增加夾檔
        /// </summary>
        /// <param name="fileName">檔案</param>
        public void Add(string fileName)
        {
            Add(fileName, Path.GetFileName(fileName));
        }
        /// <summary>
        /// 增加夾檔
        /// </summary>
        /// <param name="fileName">檔案</param>
        /// <param name="displayName">顯示的名稱</param>
        public void Add(string fileName, string displayName)
        {
            Attachment find = null;
            string fileNameTrim = fileName.Trim();
            if (!m_Attachments.TryGetValue(fileNameTrim, out find))
            {
                find = new Attachment { FileName = fileNameTrim, DisplayName = displayName };
                m_Attachments.Add(fileNameTrim, find);
            }
            else
            {
                m_Attachments[fileName].FileName = fileNameTrim;
                m_Attachments[fileName].DisplayName = displayName;
            }
        }
        /// <summary>
        ///  所有夾檔
        /// </summary>
        public Dictionary<string, Attachment>.ValueCollection Values
        {
            get { return m_Attachments.Values; }
        }
        /// <summary>
        /// 所有key (檔案路徑)
        /// </summary>
        public Dictionary<string, Attachment>.KeyCollection FileNames
        {
            get { return m_Attachments.Keys; }
        }
        public void Clear() { m_Attachments.Clear(); }

    }
}
