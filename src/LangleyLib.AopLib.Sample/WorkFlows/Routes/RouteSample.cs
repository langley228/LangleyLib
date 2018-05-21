using LangleyLib.AopLib.Sample.LogFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AopLib.Sample.WorkFlows.Routes
{
    [LogFilter]
    [FlowException("FlowException")]
    [FlowStart("Start")]
    public class RouteSample : FlowRoute
    {
        [FlowNext("Flow1")]
        public void Start()
        {
            Console.WriteLine("Run LogSample.Start");
        }
        [FlowNext("Flow2")]
        public void Flow1()
        {
            Console.WriteLine("Run LogSample.Flow1");
        }
        [FlowNext("Flow3")]
        public void Flow2()
        {
            Console.WriteLine("Run LogSample.Flow2");
        }
        [FlowNext("Flow4")]
        public void Flow3()
        {
            Console.WriteLine("Run LogSample.Flow3");
            throw new Exception("Try Exception");
        }
        [FlowNext("FlowEnd")]
        public void Flow4()
        {
            Console.WriteLine("Run LogSample.Flow4");
        }
        [FlowEnd]
        public void FlowEnd()
        {
            Console.WriteLine("Run LogSample.FlowEnd");
        }

        [FlowNext("FlowEnd")]
        public void FlowException()
        {
            Console.WriteLine("Run ExceptionFlow");
        }
    }
}
