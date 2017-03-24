using Emaill.Model;
using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Email.Service;
using Email.Util.security;
using System.IO;
using System.Drawing.Imaging;

namespace Email.Web.Controllers
{

    public class AccountController : BaseController
    {
        IAdminService _service;

        public AccountController()
        {

        }
        public AccountController(IAdminService service)
        {
            _service = service;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountUser model)
        {
            try
            {
                var user = _service.CheckUser(model.UserName, model.Password);
                _service.UpdateLoginTime(user);
                int roleid = user.RoleId.HasValue ? user.RoleId.Value : 0;
                AuthService.Login(user.Id, user.Account, user.Password,roleid);
                return JsonOK("");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }


        }
        public ActionResult GetImgVerifyChars()
        {
            HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); //不缓存
            YZMHelper yz = new YZMHelper();
            yz.CreateImage();
            HttpContext.Session.Add("CheckCode", yz.Text);
            HttpContext.Session.Timeout = 600;
            MemoryStream ms = new MemoryStream();
            yz.Image.Save(ms, ImageFormat.Png);
            yz.Image.Dispose();
            return File(ms.ToArray(), @"image/jpeg");

        }
        public ActionResult CheckVerifyCode()
        {
            string verifycode = Request.QueryString["verifycode"];
            //获取session中的找回密码验证码
            string findPwdVeridyCode = HttpContext.Session["CheckCode"] == null
                ? ""
                : HttpContext.Session["CheckCode"].ToString();
            if (verifycode != findPwdVeridyCode)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }



        }
        public ActionResult GetUserInfo()
        {
            ViewBag.UserName = UserName;
            return PartialView();
        }
        public ActionResult Logout()
        {
            AuthService.Logout();
            return View("Login");
        }
    }
}