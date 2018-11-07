using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flame.Services;
using Flame.Data;

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
            var list = _timerSchedulerServices.GetList();
            return View(list);
        }

        public ActionResult Edit(int id = 0)
        {
            var model = new TimerScheduler();
            if (id > 0)
            {
                model = _timerSchedulerServices.GetData(id);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TimerScheduler model)
        {
            if (model.Ts_Id > 0)
            {
                var entity = _timerSchedulerServices.GetData(model.Ts_Id);
                entity.Ts_Name = model.Ts_Name;
                entity.Ts_NameSpace = model.Ts_NameSpace;
                entity.Ts_RunStatu = model.Ts_RunStatu;
                entity.Ts_Interval = model.Ts_Interval;
                entity.Ts_Desc = model.Ts_Desc;
                _timerSchedulerServices.Update(entity);
            }
            else
            {
                model.Ts_AddDate = DateTime.Now;
                _timerSchedulerServices.Insert(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _timerSchedulerServices.GetData(id);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var entity = _timerSchedulerServices.GetData(id);
            if (entity?.Ts_Id > 0)
            {
                _timerSchedulerServices.Delete(entity);
            }
            return RedirectToAction("Index");
        }
    }
}