using LangleyLib.AdLib.Sample.Domains.Ldap;
using LangleyLib.AdLib.Sample.Domains.WinNT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //本機帳號
            using (var ad = new WinNTService("WinNT"))
            {
                string find = "xxx";
                Console.WriteLine("Find WinNT UserName : {0}", find);
                var uesrs = ad.WinUser.FindAll("UserName", find);
                if (uesrs != null && uesrs.Count > 0)
                {
                    foreach (var item in uesrs)
                    {
                        Console.WriteLine("UserName : {0}", item.UserName);
                        Console.WriteLine("Description : {0}", item.Description);
                        Console.WriteLine("LastLogin : {0}", item.LastLogin);
                    }
                }
            }
            //LDAP 帳號
            using (var ad = new LDAPADService("LDAP"))
            {
                string find = "xxx";
                Console.WriteLine("Find LDAP Dept1 : {0}", find);
                var uesrs = ad.LDAPUser.FindAll("Dept1", find);
                if (uesrs != null && uesrs.Count > 0)
                {
                    foreach (var item in uesrs)
                    {
                        Console.WriteLine("UserName : {0}", item.UserName);
                        Console.WriteLine("EmployeId : {0}", item.EmployeId);
                        Console.WriteLine("Email : {0}", item.Email);
                        Console.WriteLine("Dept1 : {0}", item.Dept1);
                        Console.WriteLine("Dept2 : {0}", item.Dept2);
                        Console.WriteLine("Dept3 : {0}", item.Dept3);
                        Console.WriteLine("Manager : {0}", item.Manager);
                    }
                }

            }
            Console.ReadLine();
        }
    }
}
