using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    [Authorize]
    public class LifeStyleController : BaseController
    {
        // GET: LifeStyle
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetRecordList()
        {
            var list=Service.GetRecords(0);
            return JsonOK(list);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddRecord(string content)
        {
            LifeRecord record = new LifeRecord();
            record.UserId = UserId;
            record.UserName = UserName;
            record.Content = content;
            record.CreateTime = DateTime.Now;
            Service.AddRecord(record);
            return JsonOK("ok");
        }
        [HttpPost]
        public ActionResult ClickLike(string id)
        {
            long  count = Service.ClickLike(id, UserId);
            return JsonOK(new { Count=count});
        }
    }
}