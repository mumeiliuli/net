using Email.Service;
using Email.Util.File;
using Email.Util.Front;
using Emaill.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class BaseController : Controller
    {

        public string UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                   var user= AuthService.User;
                    if (user != null)
                    {
                        return user.UserId;
                    }
                }
                return string.Empty;
            }
        }

       

        public ActionResult JsonOK(object data, string message = "", string code = "", bool result = true)
        {
            dynamic d = new DynamicData();
            d.Code = code;
            d.Result = result;
            d.Message = message;
            d.Data = data;

            return Json(d.Obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult JsonList(object rows,int total)
        {
            return Json(new { total=total,rows=rows}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult JsonError(string message = "", string code = "", bool result = false, dynamic data = null)
        {
            dynamic d = new DynamicData();
            d.Code = code;
            d.Result = result;
            d.Message = message;
            d.Data = data;

            return Json(d.Obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;

            if (Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = JsonError(ex.Message);
            }
            LogHelper.WriteLog(ex.Message, ex);
            base.OnException(filterContext);
        }
    }
}