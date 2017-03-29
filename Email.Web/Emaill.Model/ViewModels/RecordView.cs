using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emaill.Model.ViewModels
{
    public class RecordView
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public long LikeCount { get; set; }
        public long ClickCount { get; set; }

        public bool IsLike { get; set; }
        public string TimeStr { get
            {
                string time = CreateTime.ToString("HH:mm");
                DateTime dt = DateTime.Now;
                switch ((dt.Date-CreateTime.Date).Days)
                {
                    case 0: break;
                    case 1:time = "昨天 "+time; break;
                    case 2:time = "前天 " + time; break;
                    default:
                        if(CreateTime.Year == dt.Year)
                        {
                            time = CreateTime.ToString("MM月dd日 HH:mm");
                        }
                        else
                        {
                            time = CreateTime.ToString("yyyy年MM月dd日 HH:mm");
                        }
                        break;
                }
                return time;
            }
        }
    }
}
