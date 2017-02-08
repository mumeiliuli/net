using Emaill.Model;
using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service
{
    public interface IAdminService: IDependency
    {
        AccountUser CheckUser(string account, string password);
        void UpdateLoginTime(AccountUser user);

        AccountUser GetUserByID(string userid);

    }
}
