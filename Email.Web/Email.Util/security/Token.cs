using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Email.Util.security
{
    public class Token
    {
        public static string CookieName = "Email.Auth";
        public static string DomainName = "localhost";
        private readonly static int KEY = 147;
        public string UserId { get; set; }
        public string Account { get; set; }

        public int RoleId { get; set; }
        public string Password { get; set; }
        public DateTime ExpireTime { get; set; }

        public string Sign { get; set; }

        public bool IsExpired
        {
            get
            {
                return DateTime.Now > ExpireTime;
            }
        }

        public Token()
        {

        }
        public Token(string userId, string account, string password,int roleId, DateTime expireTime)
        {
            UserId = userId;
            Account = account;
            Password = password;
            RoleId = roleId;
            ExpireTime = expireTime;
        }


        public override string ToString()
        {
            string format = string.Format("{0}|{1}|{2}|{3}|{4}", UserId, Account, Password,RoleId,ExpireTime.ToString("yyyy-MM-dd HH:mm:ss"));
           
            byte[] bytes = Encoding.UTF8.GetBytes(HttpUtility.UrlEncode(format));
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte((255 - Convert.ToUInt16(bytes[i])) ^ KEY);
            }
            return Convert.ToBase64String(bytes);
        }


        public void Decode(string token)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(token);
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i]=Convert.ToByte(255 -Convert.ToUInt16(bytes[i]) ^ KEY);
                }
                string format=Encoding.UTF8.GetString(bytes);
                this.Sign = format.ToMD5();
                string[] strs= HttpUtility.UrlDecode(format).Split('|');
                if (strs.Length != 5)
                {
                    throw new Exception("token格式不正确");
                }
                UserId = strs[0];
                Account = strs[1];
                Password = strs[2];
                RoleId = Convert.ToInt16(strs[3]);
                ExpireTime =Convert.ToDateTime(strs[4]);
            }
            catch(Exception ex)
            {
                throw new Exception("token无法解析");
            }
            
        }
        public bool CheckSign(string userId, string account, string password)
        {
            string format = string.Format("{0}|{1}|{2}|{3}", userId, account, password, this.ExpireTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return HttpUtility.UrlEncode(format).MD5IsEuqls(this.Sign);

        }

    }
}
