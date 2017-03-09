using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.amqp
{
    public class EventSvc
    {
        /// <summary>
        /// 订阅列表
        /// </summary>
        private IList<Subscriber> _Subscribers = new List<Subscriber>();
        private  string routekey = "myevent";
        private ConnectionFactory connectionFactory;
        protected ConnectionFactory ConnectionFactory
        {
            get
            {
                if (null == connectionFactory)
                {
                    connectionFactory = CreateRabbitMqFactory();
                }
                return connectionFactory;
            }
        }

        public EventSvc()
        {

        }

        /// <summary>
        /// 单元测试需要通过此方法注入ConnectionFactoryy的模拟对象
        /// </summary>
        /// <param name="connectionFactory"></param>
        public EventSvc(ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventName">事件名称，唯一、推荐使用名称空间的方式命名</param>
        /// <param name="data">事件数据，推荐json格式的数据</param>
        /// <returns></returns>
        public bool FireEvent(string eventName, string data)
        {
            try
            {
                var factory = ConnectionFactory;

                using (var conn = factory.CreateConnection())
                {
                    using (var channel = conn.CreateModel())
                    {

                        channel.ExchangeDeclare(eventName, "topic", true, false, new Dictionary<string, object> { { "x-ha-policy", "all" } });
                        var body = Encoding.UTF8.GetBytes(data);

                        //持久化消息
                        var pro = channel.CreateBasicProperties();
                        pro.DeliveryMode = 2;
                        channel.BasicPublish(eventName, routekey, pro, body);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        /// <summary>
        /// 注册事件订阅者
        /// </summary>
        /// <param name="eventName">事件名称，唯一、推荐使用名称空间的方式命名</param>
        /// <param name="queueName">队列名称，唯一、推荐使用名称空间的方式命名</param>
        /// <param name="handler">订阅处理委托</param>
        public void RegisterSubscriber(string eventName, string queueName, Action<EventData> handler)
        {
            
            var subscriber = new Subscriber();
            subscriber.EventName = eventName;
            subscriber.QueueName = queueName;
            subscriber.Statu = SubscriberStatus.New;
            subscriber.Handler = handler;
            _Subscribers.Add(subscriber);
        }

        /// <summary>
        /// 运行所有监听
        /// </summary>
        /// <returns></returns>
        public void Run()
        {
            var subscriber=_Subscribers.FirstOrDefault();
            ConnectionFactory factory = ConnectionFactory;
            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    //注册excahnge
                   // channel.ExchangeDeclare(subscriber.EventName, "topic", true,false, new Dictionary<string, object> { { "x-ha-policy", "all" } });
                    var queue = channel.QueueDeclare(subscriber.QueueName, true,false,false,null);

                    //绑定queue 和 exchange
                    channel.QueueBind(queue, subscriber.EventName, routekey);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue, false, consumer);
                    //var cons = new QueueingBasicConsumer(channel);
                    //channel.BasicConsume(queue, false, cons);
                    //var q=cons.Queue.Dequeue();
                    
                    //公平分发、同一时间只处理一个消息。
                    //channel.BasicQos(0, 1, false);
                    consumer.Received += (model,r) =>
                    {
                        var message = Encoding.UTF8.GetString(r.Body);
                        Console.WriteLine(message);
                        //EventData data = new EventData();
                        //data.EventID = ea.DeliveryTag.ToString();
                        //data.Event = subscriber.EventName;
                        //data.QueueName = subscriber.QueueName;
                        //data.Data = message;
                        //try
                        //{
                        //    subscriber.Handler(data);
                        //}
                        //catch
                        //{
                        //    channel.BasicAck(ea.DeliveryTag, false);
                        //}

                    };
                    
                }
            }
            //System.Threading.Tasks.Parallel.ForEach(
            //    _Subscribers.Where(t => t.Statu == SubscriberStatus.New).ToList(),
            //    (subscriber) =>
            //    {
            //        subscriber.Statu = SubscriberStatus.Waiting;

                   

            //    });

           
        }

       


        #region private
        /// <summary>
        /// 创建MQ连接
        /// </summary>
        /// <returns></returns>
        private ConnectionFactory CreateRabbitMqFactory()
        {
            //读取配置
            string configStr = System.Configuration.ConfigurationManager.AppSettings["EventMqConn"];
            if (string.IsNullOrEmpty(configStr))
            {
                configStr = "host_name=127.0.0.1;virtual_host=/;user_name=liulimei;password=liulimei";
            }
            //读取连接配置
            configStr = configStr.Replace(" ", "");

            Dictionary<string, string> configDic = new Dictionary<string, string>();
            configStr.Split(';').ToList().ForEach(t =>
            {
                var arry = t.Split('=');
                configDic.Add(arry[0], arry[1]);
            });


            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = configDic["host_name"];
            factory.VirtualHost = configDic["virtual_host"];
            factory.UserName = configDic["user_name"];
            factory.Password = configDic["password"];

            return factory;
        }


        private IDictionary<string, uint> _MsgFailHandleCount = new Dictionary<string, uint>();
        /// <summary>
        /// 记录消息处理失败，并检查是否达到最大的失败次数
        /// </summary>
        /// <param name="key">消息唯一标识</param>
        /// <param name="maxFail">最大失败次数</param>
        /// <returns></returns>
        private bool RecordFailCountAndCheckMaxFail(string key, uint maxFail = 20)
        {
            if (_MsgFailHandleCount.ContainsKey(key))
            {
                _MsgFailHandleCount[key]++;
                return _MsgFailHandleCount[key] >= maxFail;
            }
            else
            {
                _MsgFailHandleCount.Add(key, 1);
                return false;
            }
        }
        /// <summary>
        /// 移除失败记录
        /// </summary>
        /// <param name="key"></param>
        private void RemoveFailCount(string key)
        {
            if (_MsgFailHandleCount.ContainsKey(key))
                _MsgFailHandleCount.Remove(key);
        }
        #endregion




    }
}
