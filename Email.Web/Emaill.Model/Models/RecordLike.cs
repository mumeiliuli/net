using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Models
{
    public class RecordLike
    {
        
        [MaxLength(32)]
        [Required]
        public string CommentUserId { get; set; }

        [Required]
        [MaxLength(32)]
        public string RecordId { get; set; }

        [Required]
        public bool IsCancel { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [ForeignKey("RecordId")]
        public virtual LifeRecord LifeRecord { get; set; }
    }
}
