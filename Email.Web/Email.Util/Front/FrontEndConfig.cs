using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Email.Util.Front
{
    public class FrontEndConfig

    {
        private static FrontEndConfig _config;

        /// <summary>
        /// 获取前端配置单例
        /// </summary>
        public static FrontEndConfig Instance
        {
            get
            {
                if (_config == null)
                {
                    _config = new FrontEndConfig();
                }
                return _config;
            }

        }

        public string Version
        {
            get
            {
                var version = ConfigurationManager.AppSettings["UrlArgs"];
                if (string.IsNullOrEmpty(version))
                {
                    return DateTime.Now.ToString("yyMMddHHmms");
                }else
                {
                    return version;
                }
            }
        }
        private string _root = "";
        /// <summary>
        /// 静态资源站点 
        /// </summary>
        public string Root
        {
            get
            {
                var appSetting = ConfigurationManager.AppSettings["email.root"];

                if (!string.IsNullOrWhiteSpace(appSetting) && appSetting.StartsWith("http://"))
                    return appSetting;
                else
                    return _root;
            }
            
        }

    }
}
