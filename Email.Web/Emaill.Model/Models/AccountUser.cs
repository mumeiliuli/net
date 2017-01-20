using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emaill.Model.Models
{
    [Table("AccountUser")]
    public class AccountUser
    {
        [MaxLength(32)]
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
    }
}
