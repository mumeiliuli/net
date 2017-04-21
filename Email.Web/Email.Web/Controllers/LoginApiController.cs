using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class LoginApiController : Controller
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

        public LoginApiController()
        {

        }
        // GET: LoginApi
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login()
        {
            
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "llm"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "123"));
            ClaimsIdentity indentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ticket = new AuthenticationTicket(indentity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));
            return Json(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
        }
        [HttpPost]
        public JsonResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                string name = User.Identity.Name;
                string id = User.Identity.GetUserId();
            }
            return Json("ok");
        }
    }
}