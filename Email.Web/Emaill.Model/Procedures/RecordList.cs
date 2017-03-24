using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.Procedures
{
    public class RecordList
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public long LikeCount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
