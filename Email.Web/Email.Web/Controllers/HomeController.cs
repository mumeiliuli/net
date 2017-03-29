using Email.Util.Front;
using Email.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Email.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public int NUM = 100;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["CurrentCulture"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["CurrentCulture"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CurrentCulture"].ToString());
            }
        }
        public ActionResult ChangeCulture(string ddlCulture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session["CurrentCulture"] = ddlCulture;
            return View("Index");
        }
        
        public ActionResult Index()
        {
            NUM += 1;
            ViewBag.Num = NUM;
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult _Static(string key)
        {
            ViewBag.Key = key;
            return PartialView();
        }
    }
}