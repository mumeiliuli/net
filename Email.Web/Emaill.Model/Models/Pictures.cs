using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Models
{
    public class Pictures
    {
        [MaxLength(32)]
        [Required]
        public string Id { get; set; }

        [MaxLength(32)]
        [Required]
        public string AlbumId { get; set; }

        [Required]
        public DateTime UploadTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Url { get; set; }

        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }
    }
}
