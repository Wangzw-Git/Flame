using Flame.Core.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flame.Core;
using Newtonsoft.Json;

namespace Flame.Web.Controllers
{
    public class PluginController : Controller
    {
        // GET: Plugin
        public ActionResult Index()
        {
           
            return View();
        }
    }
}