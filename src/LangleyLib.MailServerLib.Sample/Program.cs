using LangleyLib.MailServerLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LangleyLib.MailServerLib.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configuration.ConfigManager.SetConfig("xxx.config");
            SmtpSender send = null;
            using (send = new SmtpSender("Smtp"))
            {
                string tomail = "yourmail@mailserver.com";
                send.To.Add(tomail, "displayName");
                send.Subject = "LangleyLib.MailServerLib Test";
                send.Body = "LangleyLib.MailServerLib Test";
                //send.Attachments.Add("xxx.text", "displayName");
                send.Send();
            }

            Console.ReadLine();
        }
    }

}
