using Email.Util.File;
using Emaill.Model.Models;
using Emaill.Model.ViewModels;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service
{
    public partial class AdminService
    {
        private readonly int LIMIT=10;
        #region 生活记录与评论
        public void AddRecord(LifeRecord record)
        {
            record.Id = GenerateId;
            _db.LifeRecords.Add(record);
            _db.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            comment.Id = GenerateId;
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }


        public List<RecordView> GetRecords(int page,string userid)
        {

            List<RecordView> list = new List<RecordView>();
            var records = _db.LifeRecords.OrderByDescending(t => t.CreateTime).Skip(page * LIMIT).Take(LIMIT).ToList();
            MapHelper<LifeRecord, RecordView> map = new MapHelper<LifeRecord, RecordView>();
            foreach (var r in records)
            {
                RecordView view= map.AutoMap(r);
                view.LikeCount = r.RecordLike.Where(t=>!t.IsCancel).Count();
                view.IsLike = r.RecordLike.Any(t => t.CommentUserId == userid&&!t.IsCancel);
                view.ClickCount = _db.Comments.Where(t => t.ParentId == r.Id).Count();
                list.Add(view);
            }
            return list;
        }

        public List<RecordView> GetCommentss(int page, string recordid)
        {

            List<RecordView> list = new List<RecordView>();
            var comments = _db.Comments.Where(t=>t.ParentId==recordid).OrderByDescending(t => t.CreateTime).Skip(page * LIMIT).Take(LIMIT).ToList();
            MapHelper<Comment, RecordView> map = new MapHelper<Comment, RecordView>();
            list=map.AutoMapList(comments);
            return list;
        }

        public bool ClickLike(string recordid,string userid)
        {
            string setid = "r" + recordid;
            bool like = true;
            if (client.SetContainsItem(setid,userid))
            {
                like = false;
                client.RemoveItemFromSet(setid, userid);
            }
            else
            {
                client.AddItemToSet(setid, userid);
            }
            client.PublishMessage("recordlike", setid + "|" + userid+"|"+like);
            return like;
        }

        public void UpdateLike()
        {
            var clientsManager = new RedisManagerPool(RedisIP+":"+RedisPort);
            var redisPubSub = new RedisPubSubServer(clientsManager, "recordlike")
            {
                OnMessage = (channel, message) => {
                    string[] strs=message.Split('|');
                    string userid = strs[1];
                    string recordid = strs[0].TrimStart('r');
                    var like=_db.RecordLikes.FirstOrDefault(t => t.CommentUserId == userid && t.RecordId == recordid);
                    if (like == null)
                    {
                        like = new RecordLike();
                        like.CommentUserId = userid;
                        like.RecordId = recordid;
                        like.CreateTime = DateTime.Now;
                        like.IsCancel = false;
                        _db.RecordLikes.Add(like);
                    }else if (Convert.ToBoolean(strs[2]))
                    {
                        like.IsCancel = false;

                    }else
                    {
                        like.IsCancel = true;
                    }
                    _db.SaveChanges();
                }
            }.Start();

        }
        #endregion
    }
}
