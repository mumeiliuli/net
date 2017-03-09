using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Models
{
    public class Album
    {
        public Album()
        {
            Pictures = new HashSet<Pictures>();
        }
        [MaxLength(32)]
        [Required]
        public string Id { get; set; }

        [MaxLength(32)]
        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsOpen { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [ForeignKey("UserId")]
        public virtual AccountUser AccountUser { get; set; }
        public virtual ICollection<Pictures> Pictures { get; set; }
    }
}
