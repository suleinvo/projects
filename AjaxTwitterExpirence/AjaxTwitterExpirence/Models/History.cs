using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AjaxTwitterExpirence.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        [NotMapped]
        public HtmlString Link { get; set; }
    }
}