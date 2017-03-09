using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Email.Web.Models
{
    public class Suggestion
    {
        [Display(Name = "Content", ResourceType = typeof(Email.International.Resource))]
        [Required(ErrorMessageResourceName = "ContentRequired", ErrorMessageResourceType = typeof(Email.International.Resource))]
        public string Content { get; set; }
    }
}