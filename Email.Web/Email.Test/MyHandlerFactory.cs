using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Email.Test
{
    public class MyHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            string fname = url.Substring(url.IndexOf("/") + 1);
            string cname = "Email.Test." + fname.Substring(0, fname.IndexOf("."));
            object h = null;
            try
            {
                h = Activator.CreateInstance(Type.GetType(cname));
            }catch(Exception ex)
            {
                throw new HttpException("工厂不能为类型" + cname + "创建实例。", ex);
            }
            return (IHttpHandler)h;
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            
        }
    }
}