using Email.Util.security;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Web.security
{
    public static class AuthExtend
    {
        public static void UseAuthCookie(this IAppBuilder app, string returnurl)
        {
            //Cookie 授权
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = Token.CookieName,
                CookieDomain = Token.DomainName,
                AuthenticationType = "Cookies",
                TicketDataFormat = new SWTSecureDataFormat("Cookies"),
                SlidingExpiration = false,
                LoginPath = new PathString(returnurl),
                Provider = new CookieAuthenticationProvider
                {

                    OnApplyRedirect = ctx =>
                    {
                        if (IsAjaxRequest(ctx.Request))
                        {
                            ctx.Response.StatusCode = 200;
                            string body = "{\"Code\":\"401\",\"Result\":true,\"Message\":\"need login\",\"Data\":{\"ReturnUrl\":\"" + ctx.RedirectUri + "\"}}";
                            ctx.Response.Write(body);
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }
            });
        }
        private static bool IsAjaxRequest(IOwinRequest request)
        {
            IReadableStringCollection query = request.Query;
            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            IHeaderDictionary headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }
    }
}
