using Email.Service;
using Email.Util.File;
using Emaill.Model.Models;
using Emaill.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        IAdminService _service;

        public ScheduleController()
        {

        }
        public ScheduleController(IAdminService service)
        {
            _service = service;
        }
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            var list=_service.GetSchedules();
            List<ScheduleModel> schdules = new List<ScheduleModel>();
            foreach(var s in list)
            {
                schdules.Add(ScheduleMap.AutoMap(s));
            }
            return Json(schdules, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(Schedule model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.AllDay = false;
                _service.AddSchedule(model);
            }else
            {
                model.AllDay = false;
                _service.EditSchedule(model);
            }
            return JsonOK("");
        }
    }
}