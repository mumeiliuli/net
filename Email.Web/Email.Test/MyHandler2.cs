using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Email.Test
{
    public class MyHandler2 : IHttpHandler
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
             context.Response.Write("<h1><b>工厂机制</b></h1>");
        }
    }
}