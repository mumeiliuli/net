using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Email.Test
{
    public class MyFirstHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
             context.Response.Write("<h1><b>Hello HttpHandler</b></h1>");
        }
    }
}