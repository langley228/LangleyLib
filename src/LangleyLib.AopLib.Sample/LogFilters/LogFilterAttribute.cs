using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LangleyLib.AopLib.Sample.LogFilters
{
    public class LogFilterAttribute : AopFilterAttribute
    {
        public override void OnMethodException(string className, string methodName, Exception ex)
        {
            Console.WriteLine("Error Class:{0} Method:{1} 發生例外!", className, methodName);
            Console.WriteLine("Error Class:{0} Method:{1} Exception:{2}", className, methodName, ex.Message);
            //Console.WriteLine("Debug Class:{0} Method:{1} Exception:{2}", className, methodName, ex.ToString());
        }

        public override void OnMethodAfter(string className, string methodName, object[] outArgs, object returnValue)
        {
            Console.WriteLine("Info Class:{0} Method:{1} 離開!", className, methodName);
            if (outArgs != null && outArgs.Length > 0)
            {
                for (int i = 0; i < outArgs.Length; i++)
                {
                    Console.WriteLine("Info Class:{0} Method:{1} 輸出參數值 outArgs[{2}]:{3}", className, methodName, i, outArgs[i]);
                }
            }
            if (returnValue != null && returnValue.GetType() != typeof(void))
            {
                Console.WriteLine("Info Class:{0} Method:{1} 回傳值 returnValue:{2}", className, methodName, returnValue);
            }
        }

        public override void OnMethodBefore(string className, string methodName, object[] inArgs)
        {
            Console.WriteLine("Info Class:{0} Method:{1} 進入!", className, methodName);
            if (inArgs != null && inArgs.Length > 0)
            {
                for (int i = 0; i < inArgs.Length; i++)
                {
                    Console.WriteLine("Info Class:{0} Method:{1} 輸入參數值 inArgs[{2}]:{3}", className, methodName, i, inArgs[i]);
                }
            }
        }
    }
}
