using Email.Service;
using Email.Util.File;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class NoteController : BaseController
    {
        IAdminService _service;

        public NoteController()
        {

        }
        public NoteController(IAdminService service)
        {
            _service = service;
        }
        // GET: Note
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save()
        {
            var content=Request.Form["Content"];
            return JsonOK(null,"保存成功");
        }
        [HttpPost]
        public ActionResult PicManage()
        {
            try
            {
                string aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf('/'));

                //根目录路径，相对路径
                string rootPath = "/Images/note/";
                //根目录URL
                string rootUrl = aspxUrl + rootPath;
                //图片扩展名
                string fileTypes = "gif,jpg,jpeg,png,bmp";

                string currentPath = "";
                string currentUrl = "";
                string currentDirPath = "";
                string moveupDirPath = "";

                string dirPath = Server.MapPath(rootPath);
                string dirName = Request.QueryString["dir"];
                if (!string.IsNullOrEmpty(dirName))
                {
                    if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
                    {
                        throw new Exception("Invalid Directory name.");
                    }
                    dirPath += dirName + "/";
                    rootUrl += dirName + "/";
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                }

                //根据path参数，设置各路径和URL
                string path = Request.QueryString["path"];
                path = string.IsNullOrEmpty(path) ? "" : path;
                if (path == "")
                {
                    currentPath = dirPath;
                    currentUrl = rootUrl;
                    currentDirPath = "";
                    moveupDirPath = "";
                }
                else
                {
                    currentPath = dirPath + path;
                    currentUrl = rootUrl + path;
                    currentDirPath = path;
                    moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
                }

                //排序形式，name or size or type
                string order = Request.QueryString["order"];
                order = string.IsNullOrEmpty(order) ? "" : order.ToLower();

                //不允许使用..移动到上一级目录
                if (Regex.IsMatch(path, @"\.\."))
                {
                    throw new Exception("Access is not allowed.");
                }
                //最后一个字符不是/
                if (path != "" && !path.EndsWith("/"))
                {
                    throw new Exception("Parameter is not valid.");
                }
                //目录不存在或不是目录
                if (!Directory.Exists(currentPath))
                {
                    throw new Exception("Directory does not exist.");
                }

                //遍历目录取得文件信息
                string[] dirList = Directory.GetDirectories(currentPath);
                string[] fileList = Directory.GetFiles(currentPath);

                switch (order)
                {
                    case "size":
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new SizeSorter());
                        break;
                    case "type":
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new TypeSorter());
                        break;
                    case "name":
                    default:
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new NameSorter());
                        break;
                }

                Hashtable result = new Hashtable();
                result["moveup_dir_path"] = moveupDirPath;
                result["current_dir_path"] = currentDirPath;
                result["current_url"] = currentUrl;
                result["total_count"] = dirList.Length + fileList.Length;
                List<Hashtable> dirFileList = new List<Hashtable>();
                result["file_list"] = dirFileList;
                for (int i = 0; i < dirList.Length; i++)
                {
                    DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                    Hashtable hash = new Hashtable();
                    hash["is_dir"] = true;
                    hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                    hash["filesize"] = 0;
                    hash["is_photo"] = false;
                    hash["filetype"] = "";
                    hash["filename"] = dir.Name;
                    hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dirFileList.Add(hash);
                }
                for (int i = 0; i < fileList.Length; i++)
                {
                    FileInfo file = new FileInfo(fileList[i]);
                    Hashtable hash = new Hashtable();
                    hash["is_dir"] = false;
                    hash["has_file"] = false;
                    hash["filesize"] = file.Length;
                    hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                    hash["filetype"] = file.Extension.Substring(1);
                    hash["filename"] = file.Name;
                    hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dirFileList.Add(hash);
                }
                return Json(result);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
            
        }

        [HttpPost]
        public ActionResult PicUpload()
        {
            try
            {
                //根目录路径，相对路径
                string rootPath = "/note/";
                string saveUrl = "note";

                //定义允许上传的文件扩展名
                Hashtable extTable = new Hashtable();
                extTable.Add("image", "gif,jpg,jpeg,png,bmp");
                extTable.Add("flash", "swf,flv");
                extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
                extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

                //最大文件大小
                int maxSize = 1000000;

                var imgFile = Request.Files["imgFile"];
                if (imgFile == null)
                {
                    throw new Exception("请选择文件。");
                }

                string dirPath = Server.MapPath(rootPath);
                string dirName = Request.QueryString["dir"];
                if (string.IsNullOrEmpty(dirName))
                {
                    dirName = "image";
                }
                if (!extTable.ContainsKey(dirName))
                {
                    throw new Exception("目录名不正确。");
                }

                string fileName = imgFile.FileName;
                string fileExt = Path.GetExtension(fileName).ToLower();

                if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
                {
                    throw new Exception("上传文件大小超过限制。");
                }

                if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(((string)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
                {
                    throw new Exception("上传文件扩展名是不允许的扩展名。\n只允许" + ((string)extTable[dirName]) + "格式。");
                }

                //创建文件夹
                dirPath += dirName + "/";
                saveUrl += dirName + "/";
                string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + "/";
                saveUrl += ymd + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                string newFileName = DateTime.Now.ToString("yyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                string filePath = dirPath + newFileName;

                imgFile.SaveAs(filePath);

                string fileUrl = AdminService.PICURL+ saveUrl + newFileName;

                Hashtable hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = fileUrl;
                return Json(hash);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
    }
}