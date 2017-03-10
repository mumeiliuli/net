using Email.Service;
using Email.Util.File;
using Email.Util.Front;
using Email.Util.security;
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
        private AdminService _service;
        private Token _user;

        public AdminService Service
        {
            get
            {
                if (_service == null)
                {
                    _service = new AdminService();
                }
                return _service;
            }
        }
        public Token MyUser
        {
            get
            {
                if (_user == null)
                {
                    _user = AuthService.User;
                }
                return _user;
            }
        }
        public string UserId
        {
            get
            {
               
                return MyUser.UserId;
            }
        }
        public string UserName
        {
            get
            {

                return MyUser.Account;
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