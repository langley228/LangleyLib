using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LangleyLib.AopLib.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string refS="ref Input";
            string outS;
            LogSample logsample = new LogSample();
            logsample.Sample("Input", ref refS, out outS);

            var o = new WorkFlows.Controllers.WorkFlowSample();
            Thread s = new Thread(o.Start);
            s.Start();

        }
    }
}
