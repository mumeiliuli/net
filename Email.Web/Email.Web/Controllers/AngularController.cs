using Email.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Web.Controllers
{
    public class AngularController : Controller
    {
        // GET: Angular
        public JsonResult TreeData()
        {
            List<TreeNode> item11 = new List<TreeNode>
            {
                new TreeNode {title="1日" },
                new TreeNode {title="2日" },
                new TreeNode {title="3日" },
                new TreeNode {title="4日" },
            };
            List<TreeNode> item = new List<TreeNode>
            {
                new TreeNode {title="1月",nodes=item11 ,id="11"},
                new TreeNode {title="2月",nodes=item11 ,id="12"},
                new TreeNode {title="3月",nodes=item11 ,id="13"},
                new TreeNode {title="4月",nodes=item11 ,id="14"},
            };
            TreeNode node = new TreeNode
            {
                id="1",
                title = "2017年",
                nodes = item

            };
            return Json(new List<TreeNode> { node }, JsonRequestBehavior.AllowGet);
        }
    }
}