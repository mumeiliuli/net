using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.File
{
    public class LogHelper
    {
        public LogHelper()
        {

        }

        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置   
        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 

        
        /// <summary>
        /// 写系统信息日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {

            loginfo.Info(info);

        }
        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="Error"></param>
        /// <param name="se"></param>
        public static void WriteLog(string Error, Exception se)
        {

            logerror.Error(Error, se);

        }
    }
}
