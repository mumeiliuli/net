using Emaill.Model;
using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
           
               return View();
        }

        
    }
}