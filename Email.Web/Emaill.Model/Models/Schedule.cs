using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Models
{
    public class Schedule
    {
        [MaxLength(32)]
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool AllDay { get; set; }

        [MaxLength(10)]
        public string Color { get; set; }
    }
}
