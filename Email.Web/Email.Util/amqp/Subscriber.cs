using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.amqp
{
    public class Subscriber
    {
        public Subscriber()
        {
            this.Statu = SubscriberStatus.New;
        }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 事件处理
        /// </summary>
        public Action<EventData> Handler { get; set; }


        /// <summary>
        /// 订阅者状态
        /// </summary>
        public SubscriberStatus Statu { get; set; }



    }

    /// <summary>
    /// 状态
    /// </summary>
    public enum SubscriberStatus
    {
        /// <summary>
        /// 新增的订阅者
        /// </summary>
        New,
        /// <summary>
        /// 等待消息中
        /// </summary>
        Waiting,
        /// <summary>
        /// 处理消息中
        /// </summary>
        Processing
    }
}
