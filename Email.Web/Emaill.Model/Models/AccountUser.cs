using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emaill.Model.Models
{
    [Table("AccountUser")]
    public class AccountUser
    {
        public AccountUser()
        {
            Album = new HashSet<Album>();
        }

        [MaxLength(32)]
        [Required]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Account { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }

        public virtual ICollection<Album> Album { get; set; }
    }
}
