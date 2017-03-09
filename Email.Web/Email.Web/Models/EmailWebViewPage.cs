using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Models
{
    public abstract class EmailWebViewPage: EmailWebViewPage<dynamic>
    {
    }

    public abstract class EmailWebViewPage<TModel> : WebViewPage<TModel>
    {
        private Type _type;
        protected EmailWebViewPage()
        {
            _type = typeof(Email.International.Resource);
        }
        protected virtual string I(string name)
        {
            return _type.GetProperty(name).GetValue(null).ToString();
        }
        
    }
}