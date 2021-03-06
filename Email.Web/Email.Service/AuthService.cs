﻿using Email.Util.security;
using Emaill.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Email.Service
{
    public class AuthService
    {
        public static string Login(string userid,string account,string password,int roleId,bool isRemember=false)
        {
            DateTime dt = DateTime.Now;
            DateTime expired = dt.AddDays(1);
            if (isRemember)
            {
                expired = dt.AddDays(7);
            }
            Token token = new Token(userid, account, password, roleId, expired);
            HttpCookie cookie = new HttpCookie(Token.CookieName, token.ToString());
            cookie.Expires = expired;
            cookie.Domain = Token.DomainName;
            HttpContext.Current.Response.Cookies.Add(cookie);
            return cookie.Value;
        }
        public static void Logout()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[Token.CookieName];
            cookie.Expires = DateTime.Now.AddDays(-7);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static AccountUser GetUser(string cookie)
        {
            Token token = new Token();
            token.Decode(cookie);
            if (token.IsExpired) throw new Exception("已过期");
            IAdminService service = new AdminService();
            var user = service.GetUserByID(token.UserId);
            if (user != null && token.CheckSign(user.Id, user.Account, user.Password))
            {
                return user;
            }
            throw new Exception("已过期");
        }
        public static AccountUser User
        {
            get
            {
                try
                {
                    string cookie = HttpContext.Current.Request.Cookies[Token.CookieName].Value;
                    return GetUser(cookie);
                }
                catch(Exception ex)
                {
                    return null;
                }
                
            }
        }

        

        
    }
}
