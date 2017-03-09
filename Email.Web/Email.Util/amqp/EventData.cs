using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.amqp
{
    public class EventData
    {
        /*事件发布信息*/
        /// <summary>
        /// 事件名称（最好用名称空间的形式，如： nahuo.item.changed）
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 事件发布时设置的关键字，英文句号隔开
        /// </summary>
        //public string Keywords { get; set; }



        /*事件监听端的查询信息*/
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 查询的过滤关键字
        /// </summary>
        //public string Filter { get; set; }




        /*事件内容*/
        /// <summary>
        /// 事件的队列ID
        /// </summary>
        public string EventID { get; set; }

        /// <summary>
        /// 事件的数据，一般是json格式
        /// </summary>
        public string Data { get; set; }
    }
}
