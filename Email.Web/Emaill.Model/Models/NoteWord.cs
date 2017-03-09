using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Models
{
    public class NoteWord
    {
        [MaxLength(32)]
        public string Id { get; set; }
        [MaxLength(32)]
        public string UserId { get; set; }
        public string Content { get; set; }

        public DateTime UpdateTime { get; set; }

        [ForeignKey("UserId")]
        public virtual AccountUser AccountUser { get; set; }
    }
}
