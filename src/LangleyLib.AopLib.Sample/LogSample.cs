using LangleyLib.AopLib.Sample.LogFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib.Sample
{
    [LogFilterAttribute]
    public class LogSample : AopContext
    {
        public string Sample(string strIn, ref string refS, out string outS)
        {
            refS = "ref Output";
            outS = "out Output";
            Console.WriteLine("Run LogSample.Sample");
            return "This is Sample";
        }
    }
}
