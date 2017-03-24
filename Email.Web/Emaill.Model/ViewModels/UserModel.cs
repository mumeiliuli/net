using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }

        public int RoleId { get; set; }

        public string LoginTime { get; set; }
    }
}
