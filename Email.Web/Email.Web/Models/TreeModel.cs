﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Email.Web.Models
{
    public class TreeModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string date { get; set; }
        public string iconCls { get; set; }
        public List<TreeModel> children { get; set; }
    }

    public class TreeNode
    {
        public string id { get; set; }
        public string title { get; set; }

        public List<TreeNode> nodes { get; set; }
    }
}