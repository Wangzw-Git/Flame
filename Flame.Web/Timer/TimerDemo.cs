using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentScheduler;

namespace Flame.Web
{
    public class TimerDemo : Registry
    {
        public TimerDemo()
        {
            //立即执行
            Schedule<TestJob>().ToRunNow();
        }
    }
}