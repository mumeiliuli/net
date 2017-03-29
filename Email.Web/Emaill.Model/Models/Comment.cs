using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Models
{
    public class Comment
    {
        [MaxLength(32)]
        [Required]
        public string Id { get; set; }

        [MaxLength(32)]
        [Required]
        public string UserId { get; set; }

        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(32)]
        public string ParentId { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("UserId")]
        public virtual AccountUser AccountUser { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }

    }
}
