using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Email.Util.Front
{
    public static class AnyTagExtensions
    {
        /// <summary>
        /// 返回tagName 的html标签
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="tagName"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static string AnyTag(this HtmlHelper helper, string tagName, object htmlAttributes)
        {

            var tagBuilder = new TagBuilder(tagName);
            var tagModel = TagRenderMode.Normal;

            switch (tagName)
            {
                //样式表文件
                case "link":

                    tagBuilder.MergeAttribute("rel", "stylesheet");

                    tagModel = TagRenderMode.SelfClosing;

                    break;
                default:
                    break;
            }

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            return tagBuilder.ToString(tagModel);

        }

    }
    /// <summary>
    /// 样式链接地址
    /// </summary>
    public static class CssExtensions
    {
        private static FrontEndConfig _config = FrontEndConfig.Instance;

        public static MvcHtmlString RenderCss(this HtmlHelper helper, params string[] prams)
        {
            var version = _config.Version;
            if (prams.Length == 0)
            {
                return MvcHtmlString.Create("");
            }
            List<string> listPrams = prams.ToList();
          
            StringBuilder sb = new StringBuilder();
            foreach (var p in listPrams)
            {

                sb.Append(AnyTagExtensions.AnyTag(helper, "link", new
                {
                    href = string.Format("{0}/{1}?v={2}", _config.Root, p, version)
                }
                ));

            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString RenderJs(this HtmlHelper helper, params string[] prams)
        {
            var version = _config.Version;
            if (prams.Length == 0)
            {
                return MvcHtmlString.Create("");
            }
            List<string> listPrams = prams.ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var p in listPrams)
            {

                sb.Append(AnyTagExtensions.AnyTag(helper, "script", new
                {
                    src= string.Format("{0}/{1}?v={2}", _config.Root, p, version)
                }
                ));

            }
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 网站图标链接
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString Icon(this HtmlHelper helper)
        {
            var html = AnyTagExtensions.AnyTag(helper, "link", new
            {
                href = string.Format("{0}/img/common/logo.ico", _config.Root),
                rel = "shortcut icon",
                type = "image/x-icon"
            });

            return MvcHtmlString.Create(html);
        }
    }


}
