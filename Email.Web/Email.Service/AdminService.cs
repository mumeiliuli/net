using Email.Util.security;
using Emaill.Model;
using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service
{
    public class AdminService: IAdminService
    {
        private EmailDbContext _db;

        public AdminService()
        {

        }
        public AdminService(EmailDbContext db)
        {
            _db = db;
        }

        public AccountUser CheckUser(string account,string password)
        {
            AccountUser user = _db.AccountUsers.Where(t => t.Account == account).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("用户名不存在");
            }
            if (!password.MD5IsEuqls(user.Password))
            {
                throw new Exception("密码不正确");
            }
            return user;
        }

        public AccountUser GetUserByID(string userid)
        {
            return _db.AccountUsers.Find(userid);
        }

        public void UpdateLoginTime(AccountUser user)
        {
            user.LoginTime = DateTime.Now;
            _db.Entry<AccountUser>(user).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
