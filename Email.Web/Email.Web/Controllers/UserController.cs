
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
    public class UserController : BaseController
    {
        IAdminService _service;

        public UserController()
        {

        }
        public UserController(IAdminService service)
        {
            _service = service;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            var list=_service.GetUserList();
            var models=MapHelper<AccountUser, UserModel>.AutoMapList(list);
            return JsonList(models,models.Count);
        }
        [HttpPost]
        public ActionResult Save(AccountUser model)
        {
            DateTime dt = DateTime.Now;
            if (string.IsNullOrEmpty(model.Id))
            {
                model.CreateTime = dt;
                model.LoginTime = dt;
                _service.AddUser(model);
            }else
            {
               var user= _service.GetUserByID(model.Id);
                user.Password = model.Password;
                user.UserName = model.UserName;
                user.Account = model.Account;
                _service.UpdateUser(user);
            }
            return JsonOK("ok");
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            _service.DeleteUser(id);
            return JsonOK("ok");
        }

    }
}