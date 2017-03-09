using Emaill.Model;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            return JavaScript("alert('mm')");
        }
        public ActionResult Redis()
        {
            RedisClient redisClient = new RedisClient("127.0.0.1", 6379);
            redisClient.AddItemToList("list", "sqlserver");//添加列表
            var list = redisClient.GetAllItemsFromList("list"); //获取列表
            var set=redisClient.GetAllItemsFromSet("myset"); //获取集合
            redisClient.SetEntryInHash("myhash", "sex", "remale"); //添加哈希键值对
            var dic = redisClient.GetAllEntriesFromHash("myhash"); //获取哈希集
            var name=redisClient.GetValueFromHash("myhash", "name");
            //发布消息
            var msg=System.Text.UTF8Encoding.Default.GetBytes("thanks");
            redisClient.Publish("redischat", msg);
            redisClient.PublishMessage("redischat", "my name is llm");

            

            //事务
            using (var trans = redisClient.CreateTransaction())                 
            {
                trans.QueueCommand(r => r.Set("name", "gg"));      
                trans.Commit();                                       

            }                                                             
            return JsonOK(redisClient.Get<string>("myname"));
        }

        public ActionResult Toastr()
        {
            EmailDbContext db = new EmailDbContext();
            var list=db.AccountUsers.ToList();
            db.AccountUsers.Add(new Emaill.Model.Models.AccountUser { Id = "888", UserName = "mm", Account = "mm", Password = "mm", LoginTime = DateTime.Now, CreateTime = DateTime.Now });
            var user=db.AccountUsers.FirstOrDefault(t => t.Password == "llm");
            user.UserName = "test";
            if(list.Count>0)
              db.AccountUsers.Remove(list.ElementAt(0));
            db.SaveChanges();
            return View();
        }

    }
}