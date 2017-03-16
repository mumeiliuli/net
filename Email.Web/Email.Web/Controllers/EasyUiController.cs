using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class EasyUiController : Controller
    {
        // GET: EasyUi
        public ActionResult Accordion()
        {
            return View();
        }
        public ActionResult Content()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult ComboBox()
        {
            return View();
        }
        public ActionResult ComboData()
        {
            ArrayList list = new ArrayList();
            list.Add( new {id=1,text="c#",desc= "One of the programming languages designed for the Common Language Infrastructure" , selected =true,group="c"});
            list.Add(new { id = 2, text = "JAVA", desc = "Write once, run anywhere", group = "c" });
            list.Add(new { id = 3, text = "Ruby", desc = "A dynamic, reflective, general-purpose object-oriented programming language", group = "r" });
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListData()
        {
            ArrayList list = new ArrayList();
            list.Add(new {  text = "llm",  group = "l" });
            list.Add(new {  text = "wn",group = "w" });
            list.Add(new { text = "lx", group = "l" });
            list.Add(new { text = "wsy", group = "w" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Date()
        {
            return View();
        }
        public ActionResult Draggable()
        {
            return View();
        }

        public ActionResult TextBox()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(string name,string file)
        {
            var ff= Request.Files[0];
            return Json("ok");
        }
        public ActionResult Layout()
        {
            return View();
        }
        public ActionResult LinkButton()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Pagination()
        {
            return View();
        }
        public ActionResult Slide()
        {
            return View();
        }

    }
}