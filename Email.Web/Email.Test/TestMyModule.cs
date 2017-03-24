using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Email.Test
{
    public class TestMyModule : IHttpModule
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += app_BeginRequest;
            context.EndRequest += app_EndRequest;
        }
        void app_EndRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            app.Context.Response.Write("请求结束");
        }

        void app_BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            app.Context.Response.Write("请求开始");
        }
    }
}