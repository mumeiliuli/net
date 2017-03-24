using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Procedures
{
    public class Record
    {
        public string UserId { get; set; }
        public List<RecordList> RecordList { get; set; }
    }
}
