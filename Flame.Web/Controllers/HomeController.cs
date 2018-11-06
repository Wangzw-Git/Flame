using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flame.Services;

namespace Flame.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITimerSchedulerServices _timerSchedulerServices;

        public HomeController(ITimerSchedulerServices timerSchedulerServices)
        {
            _timerSchedulerServices = timerSchedulerServices;
        }

        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
    }
}