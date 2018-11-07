using Flame.Framework;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flame.Web
{
    public class TestJob :IJob
    {
        public void Execute()
        {
            FileHelper.WriteLog("joblog", "FluentScheduler 执行TestJob");
        }
    }
}