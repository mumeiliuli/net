using Email.Util.security;
using Emaill.Model;
using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emaill.Model.ViewModels;

namespace Email.Service
{
    public class AdminService: IAdminService
    {
        private EmailDbContext _db;
        private readonly string PICURL = "http://localhost:4797/";

        public AdminService()
        {
            _db = new EmailDbContext();
        }
        
        public string GenerateId
        {
            get
            {
                return Guid.NewGuid().ToString().Replace("-", "");
            }
        }
        #region 用户
        public AccountUser CheckUser(string account,string password)
        {
            AccountUser user = _db.AccountUsers.Where(t => t.Account == account).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("用户名不存在");
            }
            if (!password.MD5IsEuqls(user.Password))
            {
                throw new Exception("密码不正确");
            }
            return user;
        }

        public AccountUser GetUserByID(string userid)
        {
            return _db.AccountUsers.Find(userid);
        }

        public void UpdateLoginTime(AccountUser user)
        {
            user.LoginTime = DateTime.Now;
            _db.Entry<AccountUser>(user).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
        #endregion

        #region 图片
        public void AddImg(string name,string userId, string[] urls)
        {
            DateTime dt = DateTime.Now;
            var album=_db.Albums.FirstOrDefault(t => t.UserId == userId && t.Name == name);
            if (album == null)
            {
                album = new Album()
                {
                    Id = GenerateId,
                    UserId=userId,
                    Name=name,
                    IsOpen=true,
                    CreateTime=dt
                };
                _db.Albums.Add(album);
                _db.SaveChanges();
                
            }
            foreach(var u in urls)
            {
                Pictures p = new Pictures
                {
                    Id = GenerateId,
                    AlbumId = album.Id,
                    Url = u,
                    UploadTime = dt
                };
                _db.Pictures.Add(p);
            }
            _db.SaveChanges();
        }

        public List<AlbumView> GetAlbumList(string userid)
        {
            List<AlbumView> albums = new List<AlbumView>();
            var pics= _db.Pictures.Where(t => t.Album.UserId == userid).GroupBy(t => t.Album.Name);
            foreach(var p in pics)
            {
                var item=p.OrderByDescending(t => t.UploadTime).FirstOrDefault();
                AlbumView album = new AlbumView
                {
                    Id = item.AlbumId,
                    Name = p.Key,
                    LastImg = PICURL+ item.Url
                };
                albums.Add(album);
            }
            return albums;
        }

        public List<Album> GetAlbums(string userid)
        {
            return _db.Albums.Where(t => t.UserId == userid).ToList();
        }

        #endregion
    }
}
