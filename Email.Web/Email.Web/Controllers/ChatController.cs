using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
           string str1= Server.MapPath(".");
            string str2 = Server.MapPath("..");
            string str3 = Server.MapPath("/");
            string str4 = Server.MapPath("/temp");
            return View();
        }
    }
}