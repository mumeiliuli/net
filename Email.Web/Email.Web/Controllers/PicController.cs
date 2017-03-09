using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Email.Util.File;
using Email.Service;
using Microsoft.AspNet.Identity;

namespace Email.Web.Controllers
{
    [Authorize]
    public class PicController : BaseController
    {
        IAdminService _service;

        public PicController()
        {

        }
        public PicController(IAdminService service)
        {
            _service = service;
        }
        // GET: Pic
        public ActionResult Index()
        {
            var list = _service.GetAlbums(UserId);
            return View(list);
        }
        public ActionResult GetList()
        {
            var list=_service.GetAlbumList(UserId);
            return JsonOK(list);
        }
        [HttpPost]
        public ActionResult Upload()
        {
            try
            {
                var file = Request.Files["img"].InputStream;
                string filepath = Server.MapPath("/Images/album/");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + ".png";
                using (StreamWriter write = new StreamWriter(filepath + filename))
                {
                    file.CopyTo(write.BaseStream);
                }
                return JsonOK(new {url= "Images/album/" + filename });
            }
            catch(Exception ex)
            {
                return JsonOK(new { error=ex.Message});
            }
            
        }
        [HttpPost]
        public ActionResult SaveImg(string name,string[] urls)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "default";
            }
            _service.AddImg(name, UserId, urls);
            return JsonOK("ok");
        }

    }
}