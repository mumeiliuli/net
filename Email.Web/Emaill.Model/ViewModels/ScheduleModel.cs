using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.ViewModels
{
    public class ScheduleModel
    {
        public string id { get; set; }

        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public bool allDay { get; set; }

        public string color { get; set; }
    }
}
