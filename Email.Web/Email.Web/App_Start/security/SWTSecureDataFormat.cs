using Email.Service;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Email.Web.security
{
    public class SWTSecureDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private string _authenticationType;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="authenticationType">身份验证方式（Cookie, Bearer）</param>
        public SWTSecureDataFormat(string authenticationType)
        {
            _authenticationType = authenticationType;
        }

        public string Protect(AuthenticationTicket data)
        {
            //直接返回加密后的token
            return data.Properties.Dictionary["Token"];
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            try
            {
                var token = AuthService.GetUser(protectedText);
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, token.Account));

                claims.Add(new Claim("UserID", token.UserId));

                var identity = new ClaimsIdentity(claims, _authenticationType);
                var pros = new AuthenticationProperties();
                pros.Dictionary.Add("UserName", token.Account);
                pros.Dictionary.Add("UserID", token.UserId);
                pros.Dictionary.Add("Token", token.ToString());

                var ticket = new AuthenticationTicket(identity, pros);
                return ticket;
            }
            catch(Exception ex)
            {
                return null;
            }
            

            
        }



    }
}
