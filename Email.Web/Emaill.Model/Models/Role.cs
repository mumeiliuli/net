using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Models
{
    public class Role
    {
        public Role()
        {
            AccountUser = new HashSet<AccountUser>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<AccountUser> AccountUser { get; set; }
    }
}
