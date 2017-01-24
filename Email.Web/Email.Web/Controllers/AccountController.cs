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
                AuthService.Login(user.Id, user.Account, user.Password);
                return JsonOK("");
            }
            catch(Exception ex)
            {
                return JsonError(ex.Message);
            }
            
            
        }


    }
}